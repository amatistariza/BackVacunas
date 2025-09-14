using System;
using System.Text.Json.Serialization;

namespace API.DTO;

public class EsquemaVacunacionListadoDto
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("tipoCarnet")] public string TipoCarnet { get; set; }

    [JsonPropertyName("registradoPAI")] public bool RegistradoPAI { get; set; }

    [JsonPropertyName("nombreCompleto")] public string NombreCompleto { get; set; }

    [JsonPropertyName("vacunaAplicada")] public string VacunaAplicada { get; set; }

    [JsonPropertyName("fechaAplicada")] public DateTime FechaAplicada { get; set; }

    [JsonPropertyName("fechaProxima")] public DateTime? FechaProxima { get; set; }

    [JsonPropertyName("responsable")] public string Responsable { get; set; }
}
