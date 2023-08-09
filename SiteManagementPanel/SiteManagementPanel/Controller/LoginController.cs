using Microsoft.AspNetCore.Mvc;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Busines;
using SiteManagementPanel.Business;
using SiteManagementPanel.Schema;


namespace SiteManagementPanel.Service.Controller
{
    [Route("panel/api/[controller]")]
    [ApiController]  
    public class LoginController : ControllerBase
    {
        private readonly ILoginService service;
        public LoginController(ILoginService service)
        {
            this.service = service;
        }

        [HttpPost("login")]
        public ApiResponse<LoginResponse> Login([FromBody] LoginRequest request)
        {
            var response = service.Login(request);

            if (response.Success)
            {
                return response;
            }

            else
            {
                return new ApiResponse<LoginResponse>(response.Message);
            }
        }
    }
}
