namespace API.Domain.Models.Esquema;

public class TipoDosis
{
    public int Id { get; set; } // Identificador único
    public string Nombre { get; set; } // Nombre del tipo de dosis (Primera Dosis, Refuerzo, etc.)
    public string Descripcion { get; set; } // Descripción adicional
}