namespace API.DTO;

public class VacunaAplicadaDetalleDto
{
    public string Vacuna { get; set; }
    public int NumeroDosis { get; set; }
    public string TipoIdentificacion { get; set; }
    public string NumeroIdentificacion { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public int Edad { get; set; }
    public DateTime FechaAplicacion { get; set; }
    
    // Campos adicionales del paciente
    public string RegimenAfiliacion { get; set; }
    public string PertenenciaEtnica { get; set; }
    public string Sexo { get; set; }
    public bool Desplazado { get; set; }
    public bool Discapacitado { get; set; }
    public bool VictimaConflicto { get; set; }
    public bool EstudiaActualmente { get; set; }
}