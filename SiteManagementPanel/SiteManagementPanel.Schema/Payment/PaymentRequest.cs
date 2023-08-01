
namespace SiteManagementPanel.Schema;

public class PaymentRequest
{
    public int ApartmentId { get; set; }
    public string Type { get; set; } 
    public decimal Amount { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
}
