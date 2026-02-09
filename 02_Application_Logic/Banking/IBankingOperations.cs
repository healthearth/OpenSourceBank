// -- filepath fintechs-exhibitu/02_Application_Logic/Banking/IBankingOperations.cs
namespace GlobalBank.Domain.Interfaces
{
    public interface IBankingOperations
    {
        Task<bool> TransactAsync(Guid from, Guid to, decimal amount);
        Task<bool> ReconcileWithPhysicalVaultAsync();
    }
}
