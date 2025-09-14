namespace API.DTO;

public class LowStockSummaryDto
{
    public int Threshold { get; set; }
    public int VaccinesBelowThreshold { get; set; }
    public int SyringesBelowThreshold { get; set; }
}
