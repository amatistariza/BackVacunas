using Newtonsoft.Json;

namespace API.DTO;

public class DosisPorVacunaDto
{
    [JsonProperty("vacuna")]
    public string Vacuna { get; set; } = string.Empty;

    [JsonProperty("dosis")]
    public int Dosis { get; set; }

    [JsonProperty("fechaAplicacion")]
    public DateTime FechaAplicacion { get; set; }
}
