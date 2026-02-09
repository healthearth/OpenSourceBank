// fintechs-exhibitu/01_Core_Domain/Entities/AuditResult.cs
namespace GlobalBank.Domain.Entities;

public class AuditResult
{
    public bool Passed { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime TimestampUtc { get; set; }
}
