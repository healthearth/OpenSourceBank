// -- filepath fintechs-exhibitu/02_Application_Logic/Banking/IBankingOperations.cs
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

        var fromBalance = await _repo.GetTotalLedgerBalanceAsync(); // simplified for now
        if (fromBalance < amount)
            return false;

        await _repo.InsertLedgerEntryAsync(
            from,
            credit: 0,
            debit: amount,
            description: "Transfer Out"
        );

        await _repo.InsertLedgerEntryAsync(
            to,
            credit: amount,
            debit: 0,
            description: "Transfer In"
        );

        return true;
    }

    public async Task<bool> ReconcileWithPhysicalVaultAsync()
    {
        var ledgerTotal = await _repo.GetTotalLedgerBalanceAsync();
        var vaultTotal  = await _repo.GetTotalPhysicalVaultValueAsync();

        return ledgerTotal == vaultTotal;
    }
}
