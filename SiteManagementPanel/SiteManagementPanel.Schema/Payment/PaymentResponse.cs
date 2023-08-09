
namespace SiteManagementPanel.Schema;

public class PaymentResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public int ApartmentNumber { get; set; }
    public string Type { get; set; } //"OrtakGider", "Aidat", "Elektrik", "Su", "Doğalgaz"
    public decimal Amount { get; set; }   
    public DateTime PaymentDate { get; set; }
}
