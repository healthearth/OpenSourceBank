// Filepath: fintechs-exhibitu/01_Core_Domain/Interfaces/IBankingRepository.cs
public interface IBankingRepository {
    // Record movement of AI$ between accounts
    Task<bool> TransactAsync(Guid senderId, Guid receiverId, decimal amount);
    
    // Safety check: Digital Balance must match Physical Vault Assets
    Task<bool> ReconcileWithPhysicalVaultAsync();
}
