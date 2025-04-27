namespace API.Domain.Models.Esquema;

public class VacunaAdministrada
{
    public int Id { get; set; } // Identificador único
    public int PacienteId { get; set; } // Relación con el paciente (pendiente agregar entidad Paciente)
    public int VacunaId { get; set; } // Relación con la vacuna
    public string LoteVacuna { get; set; } // Lote de la vacuna administrada
    public int JeringaId { get; set; } // Relación con la jeringa utilizada
    public string LoteJeringa { get; set; } // Lote de la jeringa utilizada
    public int? DiluyenteId { get; set; } // Relación con el diluyente utilizado (nullable)
    public string LoteDiluyente { get; set; } // Lote del diluyente utilizado (nullable)
    public int? SueroId { get; set; } // Relación con el suero utilizado (nullable)
    public int? FrascosUtilizados { get; set; } // Número de frascos utilizados (nullable)
    public int TipoDosisId { get; set; } // Relación con el tipo de dosis
    public DateTime FechaAdministracion { get; set; } // Fecha de administración

    // Relaciones
    public Vacuna Vacuna { get; set; }
    public Jeringa Jeringa { get; set; }
    public Diluyente Diluyente { get; set; }
    public Suero Suero { get; set; }
    public TipoDosis TipoDosis { get; set; }
     
}