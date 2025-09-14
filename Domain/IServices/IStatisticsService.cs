using System.Threading.Tasks;
using API.DTO;

namespace API.Domain.IServices;

public interface IStatisticsService
{
    Task<NurseDashboardStatisticsDto> GetNurseDashboardAsync(int lowStockThreshold = 10, int cacheSeconds = 60);
}
