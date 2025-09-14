using Newtonsoft.Json;

namespace API.DTO;

public class LowStockSummaryDto
{
    [JsonProperty("umbral")]
    public int Threshold { get; set; }

    [JsonProperty("vacunasBajoUmbral")]
    public int VaccinesBelowThreshold { get; set; }

    [JsonProperty("jeringasBajoUmbral")]
    public int SyringesBelowThreshold { get; set; }
}
