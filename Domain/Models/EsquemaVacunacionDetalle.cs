using API.Domain.Models.Esquema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Models;

public class EsquemaVacunacionDetalle
{
    [Key]
    public int Id { get; set; } // Identificador único del detalle

    [Required]
    public int EsquemaVacunacionId { get; set; } // Identificador del esquema al que pertenece
    [ForeignKey("EsquemaVacunacionId")]
    public EsquemaVacunacion EsquemaVacunacion { get; set; } // Relación con EsquemaVacunacion

    public int? VacunaId { get; set; } // Identificador de la vacuna utilizada (opcional)
    [ForeignKey("VacunaId")]
    public Vacuna? Vacuna { get; set; } // Relación con Vacuna
    public int? CantidadUtilizadaVacuna { get; set; } // Cantidad utilizada del elemento

    public int? SueroId { get; set; } // Identificador del suero utilizado (opcional)
    [ForeignKey("SueroId")]
    public Suero? Suero { get; set; } // Relación con Suero
    public int? CantidadUtilizadaSuero { get; set; } // Cantidad utilizada del elemento

    public int? DiluyenteId { get; set; } // Identificador del diluyente utilizado (opcional)
    [ForeignKey("DiluyenteId")]
    public Diluyente? Diluyente { get; set; } // Relación con Diluyente
    public int? CantidadUtilizadaDiluyente { get; set; } // Cantidad utilizada del elemento

    public int? JeringaId { get; set; } // Identificador de la jeringa utilizada (opcional)
    [ForeignKey("JeringaId")]
    public Jeringa? Jeringa { get; set; } // Relación con Jeringa
    public int? CantidadUtilizadaJeringa { get; set; } // Cantidad utilizada del elemento
    
    // Nueva propiedad para registro de fecha de aplicación
    [Required]
    public DateTime FechaAplicacion { get; set; } = DateTime.Now; // Fecha en que se aplicó esta dosis
    
    // Nueva propiedad para número de dosis aplicada
    public int NumeroDosis { get; set; } = 1; // Número de dosis aplicada (por defecto 1)
    
    // Relación con las alarmas generadas
    public ICollection<AlarmaVacunacion>? Alarmas { get; set; }
}
