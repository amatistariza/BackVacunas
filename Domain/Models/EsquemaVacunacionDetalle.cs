using API.Domain.Models.Esquema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Domain.Models;

public class EsquemaVacunacionDetalle
{
    [Key]
    public int Id { get; set; } // Identificador único del detalle

    [Required]
    public int EsquemaVacunacionId { get; set; } // Identificador del esquema al que pertenece
    [ForeignKey("EsquemaVacunacionId")]
    [JsonIgnore]
    public EsquemaVacunacion EsquemaVacunacion { get; set; } // Relación con EsquemaVacunacion

    public int? VacunaId { get; set; } // Identificador de la vacuna utilizada (opcional)
    [ForeignKey("VacunaId")]
    [JsonIgnore]
    public Vacuna Vacuna { get; set; } // Relación con Vacuna
    public int? CantidadUtilizadaVacuna { get; set; } // Cantidad utilizada del elemento

    public int? SueroId { get; set; } // Identificador del suero utilizado (opcional)
    [ForeignKey("SueroId")]
    [JsonIgnore]
    public Suero Suero { get; set; } // Relación con Suero
    public int? CantidadUtilizadaSuero { get; set; } // Cantidad utilizada del elemento

    public int? DiluyenteId { get; set; } // Identificador del diluyente utilizado (opcional)
    [ForeignKey("DiluyenteId")]
    [JsonIgnore]
    public Diluyente Diluyente { get; set; } // Relación con Diluyente
    public int? CantidadUtilizadaDiluyente { get; set; } // Cantidad utilizada del elemento

    public int? JeringaId { get; set; } // Identificador de la jeringa utilizada (opcional)
    [ForeignKey("JeringaId")]
    [JsonIgnore]
    public Jeringa Jeringa { get; set; } // Relación con Jeringa
    public int? CantidadUtilizadaJeringa { get; set; } // Cantidad utilizada del elemento
    
    // Relación con las alarmas generadas
    [JsonIgnore]
    public ICollection<AlarmaVacunacion> Alarmas { get; set; }
}
