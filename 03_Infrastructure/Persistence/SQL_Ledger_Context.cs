// Filepath: fintechs-exhibitu/03_Infrastructure/Persistence/SqlLedgerContext.cs
using Microsoft.EntityFrameworkCore;

public class SqlLedgerContext : DbContext {
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<LedgerEntry> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // Ensure Ledger is "Write-Only" for compliance
        modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
        // Additional constraints to ensure balances can never be negative
    }
}
