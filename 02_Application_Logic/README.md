🧠 The brain

# Summary 

// fintechs-exhibitu/02_Application_Logic/Banking/BankingOperations.cs

using GlobalBank.Domain.Interfaces;

namespace GlobalBank.Application.Banking;

public class BankingOperations : IBankingOperations
{
    private readonly IBankingRepository _repo;

    public BankingOperations(IBankingRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> TransactAsync(Guid from, Guid to, decimal amount)
    {
        if (amount <= 0)
            return false;

        var total = await _repo.GetLedgerTotalAsync();
        if (total < amount)
            return false;

        await _repo.InsertLedgerEntryAsync(
            from, 0, amount, "Transfer Out");

        await _repo.InsertLedgerEntryAsync(
            to, amount, 0, "Transfer In");

        return true;
    }

    public async Task<bool> ReconcileWithPhysicalVaultAsync()
    {
        var ledger = await _repo.GetLedgerTotalAsync();
        var vault  = await _repo.GetPhysicalVaultTotalAsync();

        return ledger == vault;
    }
} plus // Filepath: fintechs-exhibitu/02_Application_Logic/Compliance/AMLMonitor.cs
using GlobalBank.Domain.Entities;

public class AMLMonitor {
    private const decimal FlagThreshold = 10000.00m; // Legal limit for CTR filing

    public bool IsSuspicious(decimal amount, string currency) {
        // AI Logic: Detect if the transaction hits the $10,000 reporting threshold
        // or if it's "Structuring" (multiple small transfers to avoid the $10k limit)
        return amount >= FlagThreshold;
    }

    public void ReportToRegulators(Transaction tx) {
        // Logic to generate a Suspicious Activity Report (SAR)
    }
} plus // fintechs-exhibitu/02_Application_Logic/DTOs/AccountSummary.cs plus // fintechs-exhibitu/02_Application_Logic/DTOs/PaymentRequest.cs plus // Filepath: fintechs-exhibitu/02_Application_Logic/DTOs/PhysicalAssetDeposit.cs
namespace GlobalBank.Application.DTOs;

public record PhysicalAssetDeposit(
    string SerialNumber, 
    decimal FaceValue, 
    string CurrencyCode, // USD, VND, CRC
    string VaultLocation // e.g., "Library-Shelf-A1"
); plus // fintechs-exhibitu/02_Application_Logic/DTOs/TransactionHistoryDto.cs  plus // Filepath: fintechs-exhibitu/02_Application_Logic/Services/CapitalRegistrationService.cs

using GlobalBank.Domain.Interfaces;
using GlobalBank.Domain.Entities;

namespace GlobalBank.Application.Services;

public class CapitalRegistrationService
{
    private readonly IBankingRepository _repo;

    public CapitalRegistrationService(IBankingRepository repo)
    {
        _repo = repo;
    }

    public async Task RegisterStartupCapital(
        Guid targetAccountId,
        IEnumerable<PhysicalAssetDeposit> assets)
    {
        foreach (var asset in assets)
        {
            // 1. Convert physical currency to AI$ equivalent
            decimal aiValue = ExchangeEngine.ConvertToAiDollar(
                asset.FaceValue,
                asset.CurrencyCode);

            // 2. Register the physical asset in the vault
            await _repo.RegisterPhysicalAssetAsync(asset, targetAccountId);

            // 3. Create the ledger entry
            await _repo.InsertLedgerEntryAsync(
                accountId: targetAccountId,
                credit: aiValue,
                debit: 0m,
                description: $"Startup Capital: {asset.CurrencyCode} Serial {asset.SerialNumber}",
                physicalAssetRef: asset.SerialNumber);

            // 4. Record the capital deposit event
            await _repo.RecordCapitalDepositAsync(asset);
        }
    }
} plus // Filepath: fintechs-exhibitu/02_Application_Logic/Transfers/P2PTransferService.cs

using GlobalBank.Domain.Interfaces;

namespace GlobalBank.Application.Transfers;

public class P2PTransferService
{
    private readonly IBankingOperations _ops;
    private readonly IAmlService _aml;

    public P2PTransferService(IBankingOperations ops, IAmlService aml)
    {
        _ops = ops ?? throw new ArgumentNullException(nameof(ops));
        _aml = aml ?? throw new ArgumentNullException(nameof(aml));
    }

    public async Task<bool> TransferAsync(Guid from, Guid to, decimal amount)
    {
        // Basic validation
        if (amount <= 0)
            return false;

        if (from == to)
            return false;

        // AML check
        if (!await _aml.IsTransferAllowedAsync(from, to, amount))
            return false;

        // Execute the transfer
        return await _ops.TransactAsync(from, to, amount);
    }
}
