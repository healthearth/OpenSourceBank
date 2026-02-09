// Filepath: fintechs-exhibitu/01_Core_Domain/Entities/LedgerEntry.cs
public class LedgerEntry {
    public Guid Id { get; } = Guid.NewGuid();
    public decimal Debit { get; init; }  // Money Out
    public decimal Credit { get; init; } // Money In
    public string PhysicalAssetId { get; init; } // Link to physical cash/library asset
    public DateTime Timestamp { get; } = DateTime.UtcNow;
}
