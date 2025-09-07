using System.ComponentModel.DataAnnotations;

namespace API.DTO;

public class EsquemaVacunacionCreateDTO
{
    [Required] public string TipoCarnet { get; set; }
    [Required] public string Responsable { get; set; }
    [Required] public bool RegistradoPAI { get; set; }
    public string MotivoIngreso { get; set; }
    public string Observaciones { get; set; }
    [Required] public int PacienteId { get; set; }
    [Required] public int VacunaId { get; set; }
    [Required] public int NumeroDeDosis { get; set; }
    // Fecha se asignará automáticamente en el servidor
    public string ViaDeAdministracion { get; set; }
    public string SitioDeAplicacion { get; set; }
    public string Lote { get; set; }
    // Insumos consumidos
    [Required] public List<EsquemaVacunacionDetalleCreateDTO> Detalles { get; set; } = new();
}

public class EsquemaVacunacionDetalleCreateDTO
{
    public int? VacunaId { get; set; }
    public int? CantidadUtilizadaVacuna { get; set; }
    public int? DiluyenteId { get; set; }
    public int? CantidadUtilizadaDiluyente { get; set; }
    public int? JeringaId { get; set; }
    public int? CantidadUtilizadaJeringa { get; set; }
}
