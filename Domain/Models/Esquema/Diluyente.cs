namespace API.Domain.Models.Esquema;

public class Diluyente
{
    public int Id { get; set; } // Identificador único
    public string Nombre { get; set; } // Nombre del diluyente
    public string Lote { get; set; } // Lote del diluyente
    public int CantidadDisponible { get; set; } // Cantidad de diluyentes disponibles
}