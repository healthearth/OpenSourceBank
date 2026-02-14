//
public class MetricsService
{
    private readonly OpenLedgerDbContext _context;

    public MetricsService(OpenLedgerDbContext context)
    {
        _context = context;
    }

    public double TransactionsPerSecond()
    {
        var lastMinute = DateTime.UtcNow.AddMinutes(-1);
        var count = _context.Transactions
            .Count(t => t.Timestamp >= lastMinute);

        return count / 60.0;
    }

    public int FraudCount()
    {
        return _context.Transactions.Count(t => t.IsFraud);
    }

    public int ActiveUsers()
    {
        return _context.Users.Count(u => u.IsActive);
    }

    public decimal CurrencyVolatilityPercent()
    {
        var recent = _context.ExchangeRates
            .OrderByDescending(x => x.Timestamp)
            .Take(2)
            .ToList();

        if (recent.Count < 2) return 0;

        return ((recent[0].Rate - recent[1].Rate) / recent[1].Rate) * 100;
    }
}
