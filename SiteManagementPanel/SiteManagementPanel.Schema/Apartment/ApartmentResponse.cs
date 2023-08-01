

using SiteManagement.Data;

namespace SiteManagementPanel.Schema;

public class ApartmentResponse
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int BlockId { get; set; }
    public string BlockName { get; set; }
    public int StatusId { get; set; }
    public string StatusName { get; set; }
    public int TypeId { get; set; }
    public string TypeName { get; set; }
    public int FloorNumber { get; set; }
    public int ApartmentNumber { get; set; }

}
