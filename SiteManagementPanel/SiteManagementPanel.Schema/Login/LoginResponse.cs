

namespace SiteManagementPanel.Schema;

public class LoginResponse
{
    public DateTime ExpireTime { get; set; }
    public string AccessToken { get; set; }
    public string UserName { get; set; }
}
