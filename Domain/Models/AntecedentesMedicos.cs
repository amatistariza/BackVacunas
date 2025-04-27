using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Models;

public class AntecedentesMedicos
{
    [Key]
    public int Id { get; set; } // Identificador único del antecedente médico

    [Required]
    public bool ContraindicacionVacunacion { get; set; } // ¿Sufre o ha sufrido algo que contraindique la vacunación?

    [StringLength(500)]
    public string DetalleContraindicacion { get; set; } // Detalle de la contraindicación (opcional)

    [Required]
    public bool ReaccionBiologicos { get; set; } // ¿Ha presentado reacción moderada o severa a biológicos anteriores?

    [StringLength(500)]
    public string DetalleReaccion { get; set; } // Detalle de la reacción a biológicos (opcional)

    // Relación 1:1 con Paciente
    [Required]
    public int PacienteId { get; set; } // Identificador del paciente asociado
    [ForeignKey("PacienteId")]
    public Paciente Paciente { get; set; } // Relación con la entidad Paciente
}