#nullable enable annotations
using Newtonsoft.Json;

namespace API.DTO;

public class AlarmaProximaDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("tipoIdentificacion")]
    public string TipoIdentificacion { get; set; } = string.Empty;

    [JsonProperty("numeroIdentificacion")]
    public string NumeroIdentificacion { get; set; } = string.Empty;

    [JsonProperty("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [JsonProperty("telefono")]
    public string? Telefono { get; set; }

    [JsonProperty("celular")]
    public string? Celular { get; set; }

    [JsonProperty("correo")]
    public string? Correo { get; set; }

    // "Qué debe aplicarse o qué pendiente": Dosis N de Vacuna
    [JsonProperty("pendiente")]
    public string Pendiente { get; set; } = string.Empty;

    // "Cuando hay que aplicarse"
    [JsonProperty("fechaAplicacion")]
    public DateTime FechaAplicacion { get; set; }

    // Campo OK: si ya se notificó o no
    [JsonProperty("ok")]
    public bool Ok { get; set; }
}
