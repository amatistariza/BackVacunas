using System;
using Newtonsoft.Json;

namespace API.DTO;

public class NurseDashboardStatisticsDto
{
    [JsonProperty("ultimaActualizacion")]
    public DateTime LastUpdated { get; set; }

    [JsonProperty("totalDosisVacunas")]
    public int TotalVaccineDoses { get; set; }

    [JsonProperty("totalJeringas")]
    public int TotalSyringes { get; set; }

    [JsonProperty("totalDiluyentes")]
    public int TotalDiluents { get; set; }

    [JsonProperty("aplicacionesHoy")]
    public int ApplicationsToday { get; set; }

    [JsonProperty("aplicacionesSemana")]
    public int ApplicationsThisWeek { get; set; }

    [JsonProperty("bajoStock")]
    public LowStockSummaryDto LowStock { get; set; } = new LowStockSummaryDto();
}
