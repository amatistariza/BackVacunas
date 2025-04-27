namespace API.Domain.Models.Esquema;

public class Jeringa
{
    public int Id { get; set; } // Identificador único
    public string Tipo { get; set; } // Tipo de jeringa (ej., 23G1, 22G1)
    public string Lote { get; set; } // Lote de la jeringa
    public int CantidadDisponible { get; set; } // Cantidad de jeringas disponibles
}
