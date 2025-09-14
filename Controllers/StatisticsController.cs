using System.Threading.Tasks;
using API.Domain.IServices;
using API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    // GET: api/statistics/nurse-dashboard
    [HttpGet("nurse-dashboard")]
    [ProducesResponseType(typeof(NurseDashboardStatisticsDto), 200)]
    public async Task<ActionResult<NurseDashboardStatisticsDto>> GetNurseDashboard([FromQuery] int lowStockThreshold = 10)
    {
        var stats = await _statisticsService.GetNurseDashboardAsync(lowStockThreshold);
        return Ok(stats);
    }
}
