
using System.Text.Json.Serialization;

namespace SiteManagementPanel.Schema;

public class ApartmentRequest
{
    [JsonIgnore]
    public int UserId { get; set; }
    public int BlockId { get; set; }
    public int StatusId { get; set; }
    public int TypeId { get; set; }
    public int FloorNumber { get; set; }
    public int ApartmentNumber { get; set; }
}
