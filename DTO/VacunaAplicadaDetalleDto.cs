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
}