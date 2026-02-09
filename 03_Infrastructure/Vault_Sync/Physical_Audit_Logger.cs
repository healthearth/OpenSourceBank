// Filepath: fintechs-exhibitu/03_Infrastructure/Vault/Physical_Audit_Logger.cs
using GlobalBank.Domain.Entities;

namespace GlobalBank.Infrastructure.Vault;

public class PhysicalAuditLogger 
{
    private readonly SqlLedgerContext _db;

    public PhysicalAuditLogger(SqlLedgerContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Compares physical vault inventory against digital liabilities.
    /// This is the "Truth Check" for the AI$.
    /// </summary>
    public async Task<AuditResult> PerformVaultReconciliationAsync(decimal actualPhysicalCash)
    {
        // 1. Get total Digital AI$ in circulation from SQL
        decimal totalDigitalCirculation = await _db.Accounts.SumAsync(a => a.DigitalBalance);

        // 2. Calculate Discrepancy
        decimal discrepancy = actualPhysicalCash - totalDigitalCirculation;

        var log = new VaultAuditLog
        {
            Timestamp = DateTime.UtcNow,
            DigitalTotal = totalDigitalCirculation,
            PhysicalTotal = actualPhysicalCash,
            Discrepancy = discrepancy,
            IsCompliant = discrepancy >= 0 // We must have at least 100% backing
        };

        // 3. Write to an IMMUTABLE audit table (Legal requirement)
        _db.AuditLogs.Add(log);
        await _db.SaveChangesAsync();

        return new AuditResult(log.IsCompliant, discrepancy);
    }
}
