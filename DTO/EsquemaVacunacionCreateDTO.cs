using System.ComponentModel.DataAnnotations;

namespace API.DTO;

public class EsquemaVacunacionCreateDTO
{
    [Required]
    public string TipoCarnet { get; set; } = null!;
    [Required]
    public string Responsable { get; set; } = null!;
    [Required]
    public bool RegistradoPAI { get; set; }
    public string? MotivoNoIngreso { get; set; }
    public string? Observaciones { get; set; }
    [Required]
    public int PacienteId { get; set; }
    [Required]
    public int VacunaId { get; set; }
    [Required]
    public int CantidadTotalDosis { get; set; }
    [Required]
    public string FrecuenciaAplicacion { get; set; } = null!;
    public int? DiasIntervalo { get; set; }
    public DateTime FechaPrimeraDosis { get; set; }
    [Required]
    public List<EsquemaVacunacionDetalleCreateDTO> Detalles { get; set; } = new();
}

public class EsquemaVacunacionDetalleCreateDTO
{
    public int? VacunaId { get; set; }
    public int? CantidadUtilizadaVacuna { get; set; }
    public int? SueroId { get; set; }
    public int? CantidadUtilizadaSuero { get; set; }
    public int? DiluyenteId { get; set; }
    public int? CantidadUtilizadaDiluyente { get; set; }
    public int? JeringaId { get; set; }
    public int? CantidadUtilizadaJeringa { get; set; }
    public DateTime FechaAplicacion { get; set; }
    public int NumeroDosis { get; set; } = 1;
}
