// Filepath: fintechs-exhibitu/03_Infrastructure/Persistence/SqlBankingRepository.cs
using Dapper;
using Microsoft.Data.SqlClient;
using GlobalBank.Domain.Interfaces;
using GlobalBank.Domain.Entities;

namespace GlobalBank.Infrastructure.Persistence;

public class SqlBankingRepository : IBankingRepository
{
    private readonly string _connectionString;

    public SqlBankingRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task RegisterPhysicalAssetAsync(
        PhysicalAssetDeposit asset,
        Guid targetAccountId)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var tx = conn.BeginTransaction();

        try
        {
            await conn.ExecuteAsync(
                @"INSERT INTO PhysicalVault (SerialNumber, CurrencyCode, FaceValue)
                  VALUES (@SerialNumber, @CurrencyCode, @FaceValue)",
                asset, tx);

            await conn.ExecuteAsync(
                @"INSERT INTO DigitalLedger 
                  (AccountId, Credit, Debit, PhysicalAssetRef, Description)
                  VALUES (@AccountId, @Credit, 0, @AssetRef, 'Physical Asset Deposit')",
                new
                {
                    AccountId = targetAccountId,
                    Credit = asset.FaceValue,
                    AssetRef = asset.SerialNumber
                },
                tx);

            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public async Task InsertLedgerEntryAsync(
        Guid accountId,
        decimal credit,
        decimal debit,
        string description,
        string? physicalAssetRef = null)
    {
        using var conn = new SqlConnection(_connectionString);

        await conn.ExecuteAsync(
            @"INSERT INTO DigitalLedger 
              (AccountId, Credit, Debit, Description, PhysicalAssetRef)
              VALUES (@AccountId, @Credit, @Debit, @Description, @PhysicalAssetRef)",
            new { accountId, credit, debit, description, physicalAssetRef });
    }

    public async Task<decimal> GetTotalLedgerBalanceAsync()
    {
        using var conn = new SqlConnection(_connectionString);
        return await conn.ExecuteScalarAsync<decimal>(
            @"SELECT SUM(Credit - Debit) FROM DigitalLedger");
    }

    public async Task<decimal> GetTotalPhysicalVaultValueAsync()
    {
        using var conn = new SqlConnection(_connectionString);
        return await conn.ExecuteScalarAsync<decimal>(
            @"SELECT SUM(FaceValue) FROM PhysicalVault");
    }
}
