// Filepath: fintechs-exhibitu/02_Application_Logic/Transfers/P2PTransferService.cs
public class P2PTransferService {
    private readonly IBankingRepository _repo;
    private readonly AMLMonitor _aml;

    public async Task<bool> ExecuteTransferAsync(Guid sender, Guid receiver, decimal amount) {
        if (_aml.IsSuspicious(amount, "AI$")) {
            throw new Exception("Compliance Hold: Transaction exceeds safety limits.");
        }

        // The "Handshake": Move digital AI$ while recording the physical asset backup
        return await _repo.TransactAsync(sender, receiver, amount);
    }
}
