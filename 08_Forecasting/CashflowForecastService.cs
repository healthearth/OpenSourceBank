//
public class CashflowForecastService
{
    public List<decimal> Forecast(
        List<decimal> historicalDailyTotals,
        int daysAhead = 30)
    {
        var avg = historicalDailyTotals.Average();

        return Enumerable
            .Range(1, daysAhead)
            .Select(d => avg * (1 + 0.01m * d))
            .ToList();
    }
}
