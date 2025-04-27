using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Domain.Models
{
    public class CondicionUsuaria
    {
        [Key]
        public int Id { get; set; } // Identificador único de la condición

        [Required]
        [StringLength(100)]
        public string Condicion { get; set; } // Condición de la usuaria (obligatorio)

        public bool Gestante { get; set; } // Indica si la usuaria está gestante

        public DateTime? FechaUltimaMenstruacion { get; set; } // Fecha de última menstruación (opcional)

        public int? SemanasGestacion { get; set; } // Semanas de gestación (opcional)

        public DateTime? FechaProbableParto { get; set; } // Fecha probable de parto (opcional)

        public int CantidadEmbarazosPrevios { get; set; } // Cantidad de embarazos previos

        // Relación 1:1 con Paciente
        [Required]
        public int PacienteId { get; set; } // Identificador del paciente asociado

        [ForeignKey("PacienteId")]
        public Paciente Paciente { get; set; } // Relación con la entidad Paciente
    }
}
