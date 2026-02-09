// Filepath: fintechs-exhibitu/03_Infrastructure/Persistence/SqlBankingRepository.cs
using Dapper;
using Microsoft.Data.SqlClient;
// using GlobalBank.Domain.Interfaces; // Ensures it sees the "Rules"
using GlobalBank.Domain; // Ensures it sees the "Rules"
using GlobalBank.Application.DTOs;   // Ensures it sees the "Deposit Form"

namespace GlobalBank.Infrastructure.Persistence;

public class SqlBankingRepository : IBankingRepository 
{
    private readonly string _connectionString;

    public SqlBankingRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // --- EXISTING LOGIC: Registering your $10 and 50k Dong ---
    public async Task RegisterPhysicalAssetAsync(PhysicalAssetDeposit asset, Guid targetAccountId) 
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var transaction = conn.BeginTransaction();

        try {
            var vaultSql = @"INSERT INTO PhysicalVault (SerialNumber, CurrencyCode, FaceValue) 
                             VALUES (@SerialNumber, @CurrencyCode, @FaceValue)";
            await conn.ExecuteAsync(vaultSql, asset, transaction);

            var ledgerSql = @"INSERT INTO DigitalLedger (AccountId, Credit, PhysicalAssetRef, Description) 
                              VALUES (@AccountId, @Credit, @AssetRef, 'Startup Capital Deposit')";
            
            await conn.ExecuteAsync(ledgerSql, new { 
                AccountId = targetAccountId, 
                Credit = asset.FaceValue, 
                AssetRef = asset.SerialNumber 
            }, transaction);

            transaction.Commit();
        } catch {
            transaction.Rollback();
            throw; 
        }
    }

    // --- NEW PLACEHOLDERS: Added to satisfy the Interface (Fixes CS0535) ---
    
    public async Task<bool> TransactAsync(Guid from, Guid to, decimal amount) 
    {
        // This will be the P2P transfer logic later
        throw new NotImplementedException();
    }

    public async Task<bool> ReconcileWithPhysicalVaultAsync() 
    {
        // This will be the Audit logic later
        throw new NotImplementedException();
    }
}
