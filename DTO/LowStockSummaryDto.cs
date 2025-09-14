using System.Text.Json.Serialization;

namespace API.DTO;

public class LowStockSummaryDto
{
    [JsonPropertyName("umbral")]
    public int Threshold { get; set; }

    [JsonPropertyName("vacunasBajoUmbral")]
    public int VaccinesBelowThreshold { get; set; }

    [JsonPropertyName("jeringasBajoUmbral")]
    public int SyringesBelowThreshold { get; set; }
}
