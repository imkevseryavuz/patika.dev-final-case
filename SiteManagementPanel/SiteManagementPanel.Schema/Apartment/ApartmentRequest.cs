namespace SiteManagementPanel.Schema;

public class ApartmentRequest
{
    public int UserId { get; set; }
    public int BuildingId { get; set; }
    public int Status { get; set; }
    public int ApartmentTypeId { get; set; }
    public int FloorNumber { get; set; }
    public int ApartmentNumber { get; set; }  
}
public class UpdateApartmentRequest
{
    public int UserId { get; set; }
    public int Status { get; set; }
}