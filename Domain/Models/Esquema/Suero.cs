namespace API.Domain.Models.Esquema;

public class Suero
{
    public int Id { get; set; } // Identificador único
    public string Nombre { get; set; } // Nombre del suero (ej., Antirrábico Humano)
    public string Lote { get; set; } // Lote del suero
    public int FrascosDisponibles { get; set; } // Número de frascos disponibles
}
