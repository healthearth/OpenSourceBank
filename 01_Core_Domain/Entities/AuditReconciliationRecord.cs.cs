// fintechs-exhibitu/01_Core_Domain/Entities/AuditReconciliationRecord.cs
namespace GlobalBank.Domain.Entities;

public class AuditReconciliationRecord //Former AuditResult.cs interference
{
    public Guid Id { get; set; }
    public bool Passed { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime TimestampUtc { get; set; }
}
