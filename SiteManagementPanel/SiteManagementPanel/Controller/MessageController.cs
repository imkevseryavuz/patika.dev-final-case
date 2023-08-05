using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Business;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Service.Controller
{
    [Route("panel/api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public ApiResponse<List<MessageResponse>> GetAll()
        {
            var response = _messageService.GetAll();
            return response;
        }

        [HttpPost]
        public ApiResponse Post([FromBody] MessageRequest request)
        {
            var response = _messageService.Insert(request);
            return response;
        }
    }
}
