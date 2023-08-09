
namespace SiteManagementPanel.Schema;

public class CreditCardRequest
{
    public string CardNumber { get; set; }
    public string CardHolderName { get; set; }
    public string ExpiryMonth { get; set; }
    public string ExpiryYear { get; set; }
    public string CVV { get; set; }
    public int ApartmentUserId { get; set; }
    public int BillId { get; set;}
}
