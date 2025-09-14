using System;
using Newtonsoft.Json;

namespace API.DTO;

public class EsquemaVacunacionListadoDto
{
    [JsonProperty("tipoIdentificacion", Order = 1)] public string TipoIdentificacion { get; set; }

    [JsonProperty("numeroIdentificacion", Order = 2)] public string NumeroIdentificacion { get; set; }

    [JsonProperty("tipoCarnet", Order = 3)] public string TipoCarnet { get; set; }

    [JsonProperty("registradoPAI", Order = 4)] public bool RegistradoPAI { get; set; }

    [JsonProperty("vacunaAplicada", Order = 5)] public string VacunaAplicada { get; set; }

    [JsonProperty("fechaAplicada", Order = 6)] public DateTime FechaAplicada { get; set; }

    [JsonProperty("fechaProxima", Order = 7)] public DateTime? FechaProxima { get; set; }

    [JsonProperty("responsable", Order = 8)] public string Responsable { get; set; }
}
