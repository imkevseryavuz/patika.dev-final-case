
using Microsoft.AspNetCore.Mvc;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Business;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Service.Controller
{
    [Route("panel/api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        public UserController(IUserService service)
        {
            this.service = service;
        }
        [HttpGet]
        public ApiResponse<List<UserResponse>> GetAll()
        {
            var response = service.GetAll();
            return response;
        }

        [HttpGet("{id}")]
        public ApiResponse<UserResponse> Get(int id)
        {
            var response = service.GetById(id);
            return response;
        }


        [HttpPost]
        public ApiResponse Post([FromBody] UserRequest request)
        {
            var response = service.Insert(request);
            return response;
        }

        [HttpPut("{id}")]
        public ApiResponse Put(int id, [FromBody] UserRequest request)
        {

            var response = service.Update(id, request);
            return response;
        }


        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            var response = service.Delete(id);
            return response;
        }
    }
}
