// Filepath: fintechs-exhibitu/01_Core_Domain/Interfaces/IBankingRepository.cs
using System.Threading.Tasks;
using fintechs_exhibitu._01_Core_Domain.Entities;
using fintechs_exhibitu._01_Core_Domain.ValueObjects;

namespace fintechs_exhibitu._01_Core_Domain.Interfaces
{
    public interface IBankingRepository
    {
        Task<Account?> GetAccountAsync(string accountId);
        Task SaveAccountAsync(Account account);
        Task<AuditResult> LogAuditAsync(AuditResult audit);
    }
}
