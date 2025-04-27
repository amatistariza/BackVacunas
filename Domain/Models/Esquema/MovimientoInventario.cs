namespace API.Domain.Models.Esquema;

public class MovimientoInventario
{
    public int Id { get; set; } // Identificador único
    public string TipoRecurso { get; set; } // Tipo del recurso (Vacuna, Jeringa, Diluyente, Suero)
    public int RecursoId { get; set; } // Relación con el recurso afectado
    public int Cantidad { get; set; } // Cantidad descontada o añadida
    public string TipoMovimiento { get; set; } // Entrada o Salida
    public DateTime FechaMovimiento { get; set; } // Fecha del movimiento
    public string Observacion { get; set; } // Observaciones adicionales
}
