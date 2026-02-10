// fintechs-exhibitu/01_Core_Domain/INterfaces/IAmlService.cs
namespace GlobalBank.Domain.Interfaces;

public interface IAmlService
{
    Task<bool> IsTransferAllowedAsync(Guid senderId, Guid receiverId, decimal amount);
}
