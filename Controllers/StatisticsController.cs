using System.Threading.Tasks;
using API.Domain.IServices;
using API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/estadisticas")]
public class StatisticsController : ControllerBase
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    // GET: api/statistics/nurse-dashboard
    [HttpGet("panel-enfermeria")]
    [ProducesResponseType(typeof(NurseDashboardStatisticsDto), 200)]
    public async Task<ActionResult<NurseDashboardStatisticsDto>> GetNurseDashboard([FromQuery] int lowStockThreshold = 10)
    {
        var stats = await _statisticsService.GetNurseDashboardAsync(lowStockThreshold);
        return Ok(stats);
    }

    // GET: api/estadisticas/dosis-por-vacuna?desde=2025-09-01&hasta=2025-09-13
    [HttpGet("dosis-por-vacuna")]
    [ProducesResponseType(typeof(IReadOnlyList<DosisPorVacunaDto>), 200)]
    public async Task<ActionResult<IReadOnlyList<DosisPorVacunaDto>>> GetDosesPerVaccine([FromQuery] DateTime? desde = null, [FromQuery] DateTime? hasta = null)
    {
        var data = await _statisticsService.GetDosesPerVaccineAsync(desde, hasta);
        return Ok(data);
    }

    // GET: api/estadisticas/vacunas-aplicadas?desde=2025-09-01&hasta=2025-09-30
    [HttpGet("vacunas-aplicadas")]
    [ProducesResponseType(typeof(IReadOnlyList<VacunaAplicadaDetalleDto>), 200)]
    public async Task<ActionResult<IReadOnlyList<VacunaAplicadaDetalleDto>>> GetVacunasAplicadas([FromQuery] DateTime? desde = null, [FromQuery] DateTime? hasta = null)
    {
        var data = await _statisticsService.GetVacunasAplicadasAsync(desde, hasta);
        return Ok(data);
    }
}
