[ApiController]
[Route("api/[controller]")]
public class MetricsController : ControllerBase
{
    private readonly MetricsService _metrics;

    public MetricsController(MetricsService metrics)
    {
        _metrics = metrics;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new {
            tps = _metrics.TransactionsPerSecond(),
            fraud = _metrics.FraudCount(),
            users = _metrics.ActiveUsers(),
            volatility = _metrics.CurrencyVolatilityPercent()
        });
    }
}
