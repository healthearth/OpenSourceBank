// Filepath: fintechs-exhibitu/02_Application_Logic/Services/CapitalRegistrationService.cs

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
}

