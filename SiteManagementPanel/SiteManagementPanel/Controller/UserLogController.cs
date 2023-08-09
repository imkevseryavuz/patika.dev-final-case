using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Business;
using SiteManagementPanel.Data;
using SiteManagementPanel.Schema;
using System.Security.Claims;

namespace SiteManagementPanel.Service.Controller
{
    [Route("panel/api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class UserLogController : ControllerBase
    {
        private readonly IUserLogService service;
        public UserLogController(IUserLogService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ApiResponse<List<UserLogResponse>> GetAll()
        {
           var username = User.Claims.Where(x => x.Type == "UserName")?.FirstOrDefault();
            var userid = (User.Identity as ClaimsIdentity).FindFirst("UserId")?.Value;
            var response = service.GetByUserSession(username?.Value);
           
            return response;
        }

        [HttpGet("{id}")]
        public ApiResponse<UserLogResponse> Get(int id)
        {
            var response = service.GetById(id);
            return response;
        }

    }
}
