// fintechs-exhibitu/01_Core_Domain/INterfaces/IAmlService.cs
// © 2026 Andrew Kieckhefer. All rights reserved.
    
namespace GlobalBank.Domain.Interfaces;

public interface IAmlService
{
    Task<bool> IsTransferAllowedAsync(Guid senderId, Guid receiverId, decimal amount);
}
