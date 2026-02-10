// fintechs-exhibitu/01_Core_Domain/ValueObjects/AuditResult.cs
namespace GlobalBank.Domain.ValueObjects;

public record AuditResult(decimal LedgerTotal, decimal PhysicalTotal);

// await _db.Accounts.SumAsync(a => a.Balance);


