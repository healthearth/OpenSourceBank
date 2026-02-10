// Filepath: fintechs-exhibitu/01_Core_Domain/ValueObjects/AuditResult.cs

namespace GlobalBank.Domain.ValueObjects;

public record AuditResult(bool IsCompliant, decimal Discrepancy)
{
    public static AuditResult Compliant() => new(true, 0m);

    public static AuditResult WithDiscrepancy(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Discrepancy cannot be negative.");

        return new AuditResult(false, amount);
    }
}
