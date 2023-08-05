using SiteManagamentPanel.Base;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Busines;

public interface ITokenService
{
    ApiResponse<TokenResponse> Login(TokenRequest request);
}
