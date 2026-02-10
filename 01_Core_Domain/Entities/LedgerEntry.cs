// Filepath: fintechs-exhibitu/01_Core_Domain/Entities/LedgerEntry.cs
namespace GlobalBank.Domain.Entities;
    
public class LedgerEntry {
    public Guid Id { get; } = Guid.NewGuid();
    public decimal Debit { get; init; }  // Money Out
    public decimal Credit { get; init; } // Money In
    public string Description { get; init; } = string.Empty;
    public string PhysicalAssetRef { get; init; } // Link to physical cash/library asset
    public string? PhysicalAssetRef { get; init; }
    public DateTime TimestampUtc { get; } = DateTime.UtcNow;
}
