using System.Threading.Tasks;
using API.DTO;

namespace API.Domain.IServices;

public interface IStatisticsService
{
    Task<NurseDashboardStatisticsDto> GetNurseDashboardAsync(int lowStockThreshold = 10, int cacheSeconds = 60);
    Task<IReadOnlyList<DosisPorVacunaDto>> GetDosesPerVaccineAsync(DateTime? desde = null, DateTime? hasta = null, int cacheSeconds = 60);
    Task<IReadOnlyList<VacunaAplicadaDetalleDto>> GetVacunasAplicadasAsync(DateTime? desde = null, DateTime? hasta = null, int cacheSeconds = 60);
}
