// Filepath: fintechs-exhibitu/02_Application_Logic/Transfers/P2PTransferService.cs
// © 2026 Andrew Kieckhefer. All rights reserved.

using GlobalBank.Domain.Interfaces;

namespace GlobalBank.Application.Transfers;

public class P2PTransferService
{
    private readonly IBankingOperations _ops;
    private readonly IAmlService _aml;

    public P2PTransferService(IBankingOperations ops, IAmlService aml)
    {
        _ops = ops ?? throw new ArgumentNullException(nameof(ops));
        _aml = aml ?? throw new ArgumentNullException(nameof(aml));
    }

    public async Task<bool> TransferAsync(Guid from, Guid to, decimal amount)
    {
        // Basic validation
        if (amount <= 0)
            return false;

        if (from == to)
            return false;

        // AML check
        if (!await _aml.IsTransferAllowedAsync(from, to, amount))
            return false;

        // Execute the transfer
        return await _ops.TransactAsync(from, to, amount);
    }
}
