using SiteManagamentPanel.Base;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Busines;

public interface ILoginService
{
    ApiResponse<LoginResponse> Login(LoginRequest request);
}
