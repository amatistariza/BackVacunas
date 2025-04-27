namespace API.Domain.Models.Esquema;

public class Vacuna
{
    public int Id { get; set; } // Identificador único
    public string Nombre { get; set; } // Nombre de la vacuna (ej., COVID19, BCG)
    public string? Laboratorio { get; set; } // Laboratorio que produce la vacuna
    public string Lote { get; set; } // Lote de la vacuna
    public int DosisDisponibles { get; set; } // Cantidad de dosis disponibles
    public DateTime FechaRegistro { get; set; } // Fecha de registro
    
    // Nuevas propiedades para el sistema de alarmas
    public int NumeroDosis { get; set; } // Número total de dosis que se deben aplicar
    public int IntervaloMeses { get; set; } // Intervalo en meses entre cada dosis
}
