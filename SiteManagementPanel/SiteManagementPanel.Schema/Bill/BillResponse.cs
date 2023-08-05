namespace SiteManagementPanel.Schema;

public class BillResponse
{
    public  int ApartmentUserId { get; set; }
    public string Type { get; set; } 
    public decimal Amount { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
    public DateTime? PaymentDate { get; set; }
}
