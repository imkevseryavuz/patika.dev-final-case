using SiteManagementPanel.Data.Domain;

namespace SiteManagementPanel.Schema;

public class ApartmentRequest
{
    public int UserId { get; set; }
    public int BuildingId { get; set; }
    public int StatusId { get; set; }
    public int TypeId { get; set; }
    public int FloorNumber { get; set; }
    public int ApartmentNumber { get; set; }
  
}
