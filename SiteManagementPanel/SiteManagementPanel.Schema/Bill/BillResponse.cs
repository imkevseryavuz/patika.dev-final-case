

using SiteManagement.Data;

namespace SiteManagementPanel.Schema;

public class BillResponse
{
    public  int ApartmentNumber { get; set; }
    public string Type { get; set; } 
    public decimal Amount { get; set; }
    public string Month { get; set; }
    public int Year { get; set; }
}
