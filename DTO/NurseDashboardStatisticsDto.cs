using System;
using System.Text.Json.Serialization;

namespace API.DTO;

public class NurseDashboardStatisticsDto
{
    [JsonPropertyName("ultimaActualizacion")]
    public DateTime LastUpdated { get; set; }

    [JsonPropertyName("totalDosisVacunas")]
    public int TotalVaccineDoses { get; set; }

    [JsonPropertyName("totalJeringas")]
    public int TotalSyringes { get; set; }

    [JsonPropertyName("totalDiluyentes")]
    public int TotalDiluents { get; set; }

    [JsonPropertyName("aplicacionesHoy")]
    public int ApplicationsToday { get; set; }

    [JsonPropertyName("aplicacionesSemana")]
    public int ApplicationsThisWeek { get; set; }

    [JsonPropertyName("bajoStock")]
    public LowStockSummaryDto LowStock { get; set; } = new LowStockSummaryDto();
}
