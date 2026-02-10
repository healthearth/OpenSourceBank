// Filepath: fintechs-exhibitu/03_Infrastructure/Vault/Physical_Audit_Logger.cs
using GlobalBank.Domain.Entities;
using GlobalBank.Domain.ValueObjects; 
using GlobalBank.Infrastructure.Persistence; 
using Microsoft.EntityFrameworkCore;

namespace GlobalBank.Infrastructure.Vault_Sync;

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
        decimal totalDigitalCirculation = await _db.Accounts.SumAsync(a => a.Balance);

        // 2. Calculate Discrepancy
        decimal discrepancy = actualPhysicalCash - totalDigitalCirculation;

        var log = new VaultAuditLog
        {
            Id = Guid.NewGuid(),
            Timestamp = DateTime.UtcNow,
            DigitalTotal = totalDigitalCirculation,
            PhysicalTotal = actualPhysicalCash,
            Discrepancy = discrepancy,
            IsCompliant = discrepancy >= 0 // We must have at least 100% backing
        };

        // 3. Write to an IMMUTABLE audit table (Legal requirement)
        _db.AuditLogs.Add(log);
        await _db.SaveChangesAsync();

        // 4. Return value-object audit result
        return new AuditResult.WithDiscrepancy(
            discrepancy < 0 ? Math.Abs(discrepancy) : 0m, 
            log.IsCompliant
        );
    }
}
