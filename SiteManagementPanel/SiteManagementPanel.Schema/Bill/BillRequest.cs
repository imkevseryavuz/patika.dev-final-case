

namespace SiteManagementPanel.Schema;

public class BillRequest
{
    public int ApartmentId { get; set; }
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public string Month { get; set; }
}
