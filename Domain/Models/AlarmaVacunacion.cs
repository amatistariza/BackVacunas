using API.Domain.Models.Esquema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Models;

public class AlarmaVacunacion
{
    [Key]
    public int Id { get; set; } // Identificador único de la alarma
    
    [Required]
    public int PacienteId { get; set; } // Identificador del paciente asociado
    [ForeignKey("PacienteId")]
    public Paciente Paciente { get; set; } // Relación con Paciente
    
    [Required]
    public int VacunaId { get; set; } // Identificador de la vacuna
    [ForeignKey("VacunaId")]
    public Vacuna Vacuna { get; set; } // Relación con Vacuna
    
    [Required]
    public int DosisActual { get; set; } // Número de dosis actual (1, 2, 3, etc.)
    
    [Required]
    public DateTime FechaPrimeraAplicacion { get; set; } // Fecha en que se aplicó la primera dosis
    
    [Required]
    public DateTime FechaUltimaAplicacion { get; set; } // Fecha en que se aplicó la última dosis
    
    [Required]
    public DateTime FechaProximaAplicacion { get; set; } // Fecha en que se debe aplicar la próxima dosis
    
    public bool EsquemaCompletado { get; set; } = false; // Indica si el esquema de vacunación está completo
    
    public bool NotificacionEnviada { get; set; } = false; // Indica si ya se envió la notificación
    
    public DateTime? FechaNotificacion { get; set; } // Fecha en que se envió la notificación
    
    [StringLength(500)]
    public string? Observaciones { get; set; } // Observaciones adicionales (opcional)
    
    // Referencia al EsquemaVacunacionDetalle que originó esta alarma
    public int? EsquemaVacunacionDetalleId { get; set; }
    [ForeignKey("EsquemaVacunacionDetalleId")]
    public EsquemaVacunacionDetalle? EsquemaVacunacionDetalle { get; set; }
}