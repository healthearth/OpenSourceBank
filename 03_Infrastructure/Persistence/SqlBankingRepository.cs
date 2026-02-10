// Filepath: fintechs-exhibitu/03_Infrastructure/Persistence/SqlBankingRepository.cs

using Dapper;
using Microsoft.Data.SqlClient;
using GlobalBank.Domain.Entities;
using GlobalBank.Domain.Interfaces;

namespace GlobalBank.Infrastructure.Persistence;

public class SqlBankingRepository : IBankingRepository
{
    private readonly string _connectionString;

    public SqlBankingRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Task RecordCapitalDepositAsync(PhysicalAssetDeposit deposit)
    {
        // TODO: persist deposit
        return Task.CompletedTask;
    }

    public async Task<Account?> GetAccountByIdAsync(Guid id)
    {
        using var conn = new SqlConnection(_connectionString);
        return await conn.QuerySingleOrDefaultAsync<Account>(
            "SELECT * FROM Accounts WHERE Id = @id",
            new { id });
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
              VALUES (@accountId, @credit, @debit, @description, @physicalAssetRef)",
            new { accountId, credit, debit, description, physicalAssetRef });
    }

    public async Task RegisterPhysicalAssetAsync(
        PhysicalAssetDeposit asset,
        Guid targetAccountId)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.ExecuteAsync(
            @"INSERT INTO PhysicalVault (SerialNumber, CurrencyCode, FaceValue)
              VALUES (@SerialNumber, @CurrencyCode, @FaceValue)",
            asset);
    }

    public async Task<decimal> GetLedgerTotalAsync()
    {
        using var conn = new SqlConnection(_connectionString);
        return await conn.ExecuteScalarAsync<decimal>(
            "SELECT ISNULL(SUM(Credit - Debit), 0) FROM DigitalLedger");
    }

    public async Task<decimal> GetPhysicalVaultTotalAsync()
    {
        using var conn = new SqlConnection(_connectionString);
        return await conn.ExecuteScalarAsync<decimal>(
            "SELECT ISNULL(SUM(FaceValue), 0) FROM PhysicalVault");
    }
}
