// Filepath: fintechs-exhibitu/03_Infrastructure/Persistence/SqlLedgerContext.cs
// © 2026 Andrew Kieckhefer. All rights reserved.

using GlobalBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlobalBank.Infrastructure.Persistence;

public class SqlLedgerContext : DbContext { 
    public DbSet<Account> Accounts { get; set; } = null!; 
    public DbSet<Transaction> Transactions { get; set; } = null!; 
    public DbSet<VaultAuditLog> AuditLogs { get; set; } = null!; 
    public DbSet<LedgerEntry> LedgerEntries { get; set; } = null!; 
    
    public SqlLedgerContext(DbContextOptions<SqlLedgerContext> options) 
        : base(options) 
    { 
    } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    { 
        modelBuilder.Entity<Transaction>().HasKey(t => t.Id); 
        modelBuilder.Entity<Account>().HasKey(a => a.Id); 
        modelBuilder.Entity<VaultAuditLog>().HasKey(v => v.Id); 
        modelBuilder.Entity<LedgerEntry>().HasKey(l => l.Id); 
        
        // TODO: add constraints to enforce non-negative balances, etc. 
    } 
}
