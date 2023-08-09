

namespace SiteManagementPanel.Schema;

public class BillRequest
{
    public int ApartmentId { get; set; }
    public int BillTypeId { get; set; }
    public decimal Amount { get; set; }

    public DateTime DueDate { get; set; }

}

public class UpdateBillRequest
{
    public int? PaymentId { get; set; }
}