
using SiteManagementPanel.Data;

namespace SiteManagementPanel.Schema;

public class ApartmentResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string BlockName { get; set; }
    public ApartmenStatusType Status { get; set; }
    public string TypeName { get; set; }
    public int FloorNumber { get; set; }
    public int ApartmentNumber { get; set; }

 

}
