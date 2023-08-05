using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Busines;
using SiteManagementPanel.Business;
using SiteManagementPanel.Schema;
using SiteManagementPanel.Service.Filters;

namespace SiteManagementPanel.Service.Controller
{
    [Route("panel/api/[controller]")]
    [ApiController]
    
    public class TokenController : ControllerBase
    {
        private readonly ITokenService service;
        public TokenController(ITokenService service)
        {
            this.service = service;
        }


        [TypeFilter(typeof(LogResourceFilter))]
        [TypeFilter(typeof(LogActionFilter))]
        [TypeFilter(typeof(LogAuthorizationFilter))]
        [TypeFilter(typeof(LogResultFilter))]
        [TypeFilter(typeof(LogExceptionFilter))]
        [HttpGet("HeartBeat")]
        public ApiResponse Get()
        {
            return new ApiResponse("Hello");
        }

        [HttpPost("login")]
        public ApiResponse<TokenResponse> Login([FromBody] TokenRequest request)
        {
            var response = service.Login(request);

            if (response.Success)
            {
                return response;
            }

            else
            {
                return new ApiResponse<TokenResponse>(response.Message);
            }
        }
    }
}
