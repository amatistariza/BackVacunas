namespace API.DTO;

public class PacienteEditDTO
{
    public int? Id { get; set; }

    public DateTime? FechaAtencion { get; set; }
    public string? TipoIdentificacion { get; set; }
    public string? NumeroIdentificacion { get; set; }
    public string? PrimerNombre { get; set; }
    public string? SegundoNombre { get; set; }
    public string? PrimerApellido { get; set; }
    public string? SegundoApellido { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public bool? EsquemaCompleto { get; set; }
    public string? Sexo { get; set; }
    public string? OrientacionSexual { get; set; }
    public int? EdadGestacionalSemanas { get; set; }
    public string? PaisNacimiento { get; set; }
    public string? EstatusMigratorio { get; set; }
    public string? RegimenAfiliacion { get; set; }
    public string? Aseguradora { get; set; }
    public string? PertenenciaEtnica { get; set; }
    public bool? Desplazado { get; set; }
    public bool? Discapacitado { get; set; }
    public bool? Fallecido { get; set; }
    public bool? VictimaConflictoArmado { get; set; }
    public bool? EstudiaActualmente { get; set; }
    public string? PaisResidencia { get; set; }
    public string? DepartamentoResidencia { get; set; }
    public string? MunicipioResidencia { get; set; }
    public string? ComunaLocalidad { get; set; }
    public string? Area { get; set; }
    public string? Direccion { get; set; }
    public string? TelefonoFijo { get; set; }
    public string? Celular { get; set; }
    public string? Email { get; set; }
    public bool? AutorizaLlamadasTelefonicas { get; set; }
    public bool? AutorizaEnvioCorreo { get; set; }

    public int? MadreId { get; set; }
    public int? CuidadorId { get; set; }
}