// -- filepath fintechs-exhibitu/02_Application_Logic/Banking/BankingOperations.cs
using GlobalBank.Domain.Interfaces;

namespace GlobalBank.Application.Banking;

public class BankingOperations : IBankingOperations
{
    private readonly IBankingRepository _repo;

    public BankingOperations(IBankingRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> TransactAsync(Guid from, Guid to, decimal amount)
    {
        if (amount <= 0)
            return false;

        var total = await _repo.GetLedgerTotalAsync();
        if (total < amount)
            return false;

        await _repo.InsertLedgerEntryAsync(
            from, 0, amount, "Transfer Out");

        await _repo.InsertLedgerEntryAsync(
            to, amount, 0, "Transfer In");

        return true;
    }

    public async Task<bool> ReconcileWithPhysicalVaultAsync()
    {
        var ledger = await _repo.GetLedgerTotalAsync();
        var vault  = await _repo.GetPhysicalVaultTotalAsync();

        return ledger == vault;
    }
}
