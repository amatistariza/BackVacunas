using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Domain.Models;

public class Antecedente
{
    [Key]
    public int Id { get; set; } // Identificador único del antecedente

    [Required]
    public DateTime FechaRegistro { get; set; } // Fecha de registro del antecedente

    [Required]
    [StringLength(50)]
    public string Tipo { get; set; } // Tipo de antecedente (ej., "Médico", "Quirúrgico")

    [Required]
    [StringLength(500)]
    public string Descripcion { get; set; } // Descripción del antecedente

    [StringLength(500)]
    public string ObservacionesEspeciales { get; set; } // Observaciones especiales (opcional)

    [Required]
    public int PacienteId { get; set; } // Identificador del paciente asociado

    [ForeignKey("PacienteId")]
    public Paciente Paciente { get; set; } // Relación con la entidad Paciente
}