namespace SiteManagementPanel.Schema;

public class BillResponse
{
    public int ApartmentUserId { get; set; }
    public string BillType { get; set; }
    public int BillTypeId { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }

}
public class PaidBillResponse
{
    public int ApartmentUserId { get; set; }
    public string BillType { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

}