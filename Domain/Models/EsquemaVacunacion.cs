using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Models;

public class EsquemaVacunacion
{
    [Key]
    public int Id { get; set; } // Identificador único del esquema

    [Required]
    [StringLength(50)]
    public string TipoCarnet { get; set; } // Tipo de carnet

    [Required]
    [StringLength(100)]
    public string Responsable { get; set; } // Responsable del esquema (vacunador)

    [Required]
    public bool RegistradoPAI { get; set; } // Indica si fue registrado en el PAI

    [StringLength(500)]
    public string? MotivoNoIngreso { get; set; } // Motivo de no ingreso (opcional)

    [StringLength(500)]
    public string? Observaciones { get; set; } // Observaciones adicionales (opcional)

    [Required]
    public int PacienteId { get; set; } // Identificador del paciente asociado

    // Relación 1:N con Detalles
    public ICollection<EsquemaVacunacionDetalle> Detalles { get; set; } // Lista de detalles del esquema
}
