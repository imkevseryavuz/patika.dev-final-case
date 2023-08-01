

namespace SiteManagementPanel.Schema;

public class UserRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string OwnerName { get; set; }
    public string TenantName { get; set; }
    public string TCNo { get; set; }
    public string Phone { get; set; }
    public string VehiclePlateNumber { get; set; }

    public string Role { get; set; }
    public int Status { get; set; } = 1;

}
