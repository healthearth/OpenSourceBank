// Filepath: fintechs-exhibitu/01_Core_Domain/Interfaces/IBankingRepository.cs
using System;
using System.Threading.Tasks;
using GlobalBank.Domain.Entities;

namespace GlobalBank.Domain.Interfaces
{
    public interface IBankingRepository
    {
        Task<Account?> GetAccountByIdAsync(Guid id);

        Task InsertLedgerEntryAsync(
            Guid accountId,
            decimal credit,
            decimal debit,
            string description,
            string? physicalAssetRef = null);

        Task RegisterPhysicalAssetAsync(
            PhysicalAssetDeposit asset,
            Guid targetAccountId);

        Task<decimal> GetLedgerTotalAsync();

        Task<decimal> GetPhysicalVaultTotalAsync();

        Task RecordCapitalDepositAsync(PhysicalAssetDeposit deposit);
    }
}
