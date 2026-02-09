// Filepath: fintechs-exhibitu/02_Application_Logic/Compliance/AMLMonitor.cs
using GlobalBank.Domain.Entities;

public class AMLMonitor {
    private const decimal FlagThreshold = 10000.00m; // Legal limit for CTR filing

    public bool IsSuspicious(decimal amount, string currency) {
        // AI Logic: Detect if the transaction hits the $10,000 reporting threshold
        // or if it's "Structuring" (multiple small transfers to avoid the $10k limit)
        return amount >= FlagThreshold;
    }

    public void ReportToRegulators(Transaction tx) {
        // Logic to generate a Suspicious Activity Report (SAR)
    }
}
