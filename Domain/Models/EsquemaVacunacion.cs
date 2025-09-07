using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using API.Domain.Models.Esquema;

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

  [Required]
  public int VacunaId { get; set; } // Identificador de la vacuna

  [Required]
  public int CantidadTotalDosis { get; set; } // Total de dosis requeridas (ej: 5)

  [Required]
  [StringLength(50)]
  public string FrecuenciaAplicacion { get; set; } // semanal, mensual, dias

  public int? DiasIntervalo { get; set; } // Días entre dosis (cuando frecuencia es "dias")

  public DateTime FechaPrimeraDosis { get; set; } // Fecha de la primera dosis

  // Relación 1:N con Detalles
  public ICollection<EsquemaVacunacionDetalle> Detalles { get; set; } // Lista de detalles del esquema

  // Navigation properties
  [ForeignKey(nameof(VacunaId))]
  [JsonIgnore]
  public virtual Vacuna Vacuna { get; set; }

  [ForeignKey(nameof(PacienteId))]
  [JsonIgnore]
  public virtual Paciente Paciente { get; set; }
}
