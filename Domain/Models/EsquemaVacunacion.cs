using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using API.Domain.Models.Esquema;

namespace API.Domain.Models;

public class EsquemaVacunacion
{
  [Key]
  public int Id { get; set; }

  [Required, StringLength(50)]
  public string TipoCarnet { get; set; }

  [Required, StringLength(100)]
  public string Responsable { get; set; }

  [Required]
  public bool RegistradoPAI { get; set; }

  [StringLength(500)]
  public string MotivoIngreso { get; set; }

  [StringLength(500)]
  public string Observaciones { get; set; }

  [Required]
  public int PacienteId { get; set; }

  [Required]
  public int VacunaId { get; set; }

  // Dosis aplicada actualmente (1,2,3...)
  [Required]
  public int NumeroDeDosis { get; set; }

  // Fecha en que se aplicó esta dosis
  [Required]
  public DateTime FechaDosisAplicada { get; set; }

  // Calculada: próxima fecha sugerida (puede ser null si última dosis)
  public DateTime? FechaProximaDosis { get; set; }

  // Datos de administración
  [StringLength(100)]
  public string ViaDeAdministracion { get; set; }
  [StringLength(100)]
  public string SitioDeAplicacion { get; set; }
  [StringLength(100)]
  public string Lote { get; set; }

  // Relación con detalles (insumos consumidos)
  public ICollection<EsquemaVacunacionDetalle> Detalles { get; set; }

  [ForeignKey(nameof(VacunaId))]
  [JsonIgnore]
  public virtual Vacuna Vacuna { get; set; }

  [ForeignKey(nameof(PacienteId))]
  [JsonIgnore]
  public virtual Paciente Paciente { get; set; }
}
