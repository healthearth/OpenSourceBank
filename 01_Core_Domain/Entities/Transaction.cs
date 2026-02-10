// Filepath: fintechs-exhibitu/01_Core_Domain/Entities/Transaction.cs
// © 2026 Andrew Kieckhefer. All rights reserved.
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
}
