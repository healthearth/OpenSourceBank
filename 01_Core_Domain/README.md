🫀 The heart and rules of the bank

# Summary 

// -- filepath fintechs-exhibitu/01_Core_Domain/Interfaces/IBankingOperations.cs
namespace GlobalBank.Domain.Interfaces;

public interface IBankingOperations
{
    Task<bool> TransactAsync(Guid senderId, Guid receiverId, decimal amount);
    Task<bool> ReconcileWithPhysicalVaultAsync();
} plus 
using System;
using System.Threading.Tasks;
using GlobalBank.Domain.Entities;

namespace GlobalBank.Domain.Interfaces
{
    public interface IBankingRepository
    {
        Task<Account?> GetAccountByIdAsync(Guid id);

        Task InsertLedgerEntryAsync(
            Guid accountId,
            decimal credit,
            decimal debit,
            string description,
            string? physicalAssetRef = null);

        Task RegisterPhysicalAssetAsync(
            PhysicalAssetDeposit asset,
            Guid targetAccountId);

        Task<decimal> GetLedgerTotalAsync();

        Task<decimal> GetPhysicalVaultTotalAsync();

        Task RecordCapitalDepositAsync(PhysicalAssetDeposit deposit);
    }
} plus // fintechs-exhibitu/01_Core_Domain/INterfaces/IAmlService.cs
namespace GlobalBank.Domain.Interfaces;

public interface IAmlService
{
    Task<bool> IsTransferAllowedAsync(Guid senderId, Guid receiverId, decimal amount);
} plus // Filepath: fintechs-exhibitu/01_Core_Domain/Entities/Account.cs

namespace GlobalBank.Domain.Entities;

public class Account
{
    public Guid Id { get; private set; }
    public string OwnerName { get; private set; }
    public string CurrencyCode { get; private set; } = "USD";
    public bool IsKycVerified { get; private set; }

    // Optional: remove Balance entirely if ledger-derived
    public decimal Balance { get; private set; }

    public Account(string owner, string currency)
    {
        Id = Guid.NewGuid();
        OwnerName = owner;
        CurrencyCode = currency;
        Balance = 0m;
    }

    public void MarkKycVerified()
    {
        IsKycVerified = true;
    }

    // Optional: controlled balance mutation
    public void ApplyDelta(decimal amount)
    {
        Balance += amount;
    }
} pllus // fintechs-exhibitu/01_Core_Domain/Entities/AuditResult.cs
namespace GlobalBank.Domain.Entities;

public class AuditResult
{
    public bool Passed { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime TimestampUtc { get; set; }
} pluis // Filepath: fintechs-exhibitu/01_Core_Domain/Entities/LedgerEntry.cs
public class LedgerEntry {
    public Guid Id { get; } = Guid.NewGuid();
    public decimal Debit { get; init; }  // Money Out
    public decimal Credit { get; init; } // Money In
    public string Description { get; set; } = string.Empty;
    public string PhysicalAssetId { get; init; } // Link to physical cash/library asset
    public Guid? PhysicalAssetId { get; set; }
    public DateTime Timestamp { get; } = DateTime.UtcNow;
} plus  // Filepath: fintechs-exhibitu/01_Core_Domain/Entities/PhysicalAssetDeposit.cs

namespace GlobalBank.Domain.Entities;

public class PhysicalAssetDeposit
{
    public Guid Id { get; private set; }
    public decimal FaceValue { get; private set; }
    public string CurrencyCode { get; private set; }
    public string SerialNumber { get; private set; }
    public DateTime DepositedAtUtc { get; private set; }

    public PhysicalAssetDeposit(
        decimal faceValue,
        string currencyCode,
        string serialNumber)
    {
        Id = Guid.NewGuid();
        FaceValue = faceValue;
        CurrencyCode = currencyCode;
        SerialNumber = serialNumber;
        DepositedAtUtc = DateTime.UtcNow;
    }
}  plus // Filepath: fintechs-exhibitu/01_Core_Domain/Entities/Transaction.cs
namespace GlobalBank.Domain.Entities;

public class Transaction {
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid FromAccountId { get; set; }
    public Guid ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public string CurrencyCode { get; set; }
    public string PhysicalAssetReference { get; set; } // Link to specific library asset
    public DateTime TimestampUtc { get; set; }

    public Transaction(Guid from, Guid to, decimal amount, string currency, string assetRef) {
        if (amount <= 0) throw new ArgumentException("Amount must be positive.");
        FromAccountId = from;
        ToAccountId = to;
        Amount = amount;
        CurrencyCode = currency;
        PhysicalAssetReference = assetRef;
    }
} plus // fintechs-exhibitu/01_Core_Domain/Entities/VaultAuditLogs.cs
namespace GlobalBank.Domain.Entities;

public class VaultAuditLog
{
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    public decimal DigitalTotal { get; set; }
    public decimal PhysicalTotal { get; set; }
    public decimal Discrepancy { get; set; }
    public bool IsCompliant { get; set; }
} plus // fintechs-exhibitu/01_Core_Domain/Services/ExchangeEngine.cs
namespace GlobalBank.Domain.Services;

public static class ExchangeEngine
{
    public static decimal Convert(decimal amount, string currencyCode)
        => amount; // placeholder until real FX logic
} plus // Filepath: fintechs-exhibitu/01_Core_Domain/ValueObjects/AuditResult.cs

namespace GlobalBank.Domain.ValueObjects;

public record AuditResult(bool IsCompliant, decimal Discrepancy)
{
    public static AuditResult Compliant() => new(true, 0m);

    public static AuditResult WithDiscrepancy(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Discrepancy cannot be negative.");

        return new AuditResult(false, amount);
    }
} plus // Filepath: fintechs-exhibitu/01_Core_Domain/ValueObjects/Currency.cs
namespace GlobalBank.Domain.ValueObjects;

public record Currency {
    public string Code { get; } // e.g., "AI$"
    public string Symbol { get; } // e.g., "🤖"
    
    public static Currency AiDollar => new Currency("AI$", "🤖");
    public static Currency Usd => new Currency("USD", "$");

    private Currency(string code, string symbol) {
        Code = code;
        Symbol = symbol;
    }
} plus // Filepath: fintechs-exhibitu/01_Core_Domain/ValueObjects/Money.cs
public record Money {
    public decimal Amount { get; init; }
    public string Currency { get; init; } // e.g. "AI$"

    public Money(decimal amount, string currency) {
        if (amount < 0) throw new ArgumentException("Money cannot be negative");
        Amount = Math.Round(amount, 2); // Regulatory standard precision
        Currency = currency;
    }
}  plus // fintechs-exhibitu/01_Core_Domain/Interfaces/IAuditLogger.cs
