using System;

namespace API.DTO;

public class NurseDashboardStatisticsDto
{
    public DateTime LastUpdated { get; set; }

    public int TotalVaccineDoses { get; set; }
    public int TotalSyringes { get; set; }

    public int ApplicationsToday { get; set; }
    public int ApplicationsThisWeek { get; set; }

    public LowStockSummaryDto LowStock { get; set; } = new LowStockSummaryDto();
}
