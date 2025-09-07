using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Domain.Models.Esquema;

namespace API.Domain.Models
{
    public class RegistroVacunacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PacienteId { get; set; }

        [Required]
        public int VacunaId { get; set; }

        [Required]
        public int EsquemaVacunacionId { get; set; }

        [Required]
        public int NumeroDosis { get; set; }

        [Required]
        public DateTime FechaAplicacion { get; set; }

        public DateTime? FechaProximaDosis { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }

        [Required]
        [StringLength(50)]
        public string EstadoRegistro { get; set; } // Aplicada, Programada, Vencida

        [StringLength(500)]
        public string Observaciones { get; set; }

        [Required]
        [StringLength(100)]
        public string UsuarioRegistro { get; set; }

        // Navigation properties
        [ForeignKey(nameof(PacienteId))]
        public virtual Paciente Paciente { get; set; }

        [ForeignKey(nameof(VacunaId))]
        public virtual Vacuna Vacuna { get; set; }

        [ForeignKey(nameof(EsquemaVacunacionId))]
        public virtual EsquemaVacunacion EsquemaVacunacion { get; set; }
    }
}
