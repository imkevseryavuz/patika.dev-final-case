using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Business;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Service.Controller
{
    [Route("panel/api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        [HttpPost]
        public IActionResult SendMessage([FromBody] MessageRequest messageRequest)
        {
            var response = _messageService.SendMessage(messageRequest);
            return Ok(new ApiResponse<MessageResponse>(response.Message));
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetMessagesByUserId(int userId)
        {
            var response = _messageService.GetMessagesByUserId(userId);
            return Ok(new ApiResponse<List<MessageResponse>>(response.Message));
        }

        [HttpGet("{messageId}")]
        public IActionResult GetMessageById(int messageId)
        {
            var response = _messageService.GetMessageById(messageId);
            return response.Success ? Ok(response.Message) : NotFound(response.Message);
        }

        [HttpPost("{messageId}/mark-as-read")]
        public IActionResult MarkMessageAsRead(int messageId)
        {
            var response = _messageService.MarkMessageAsRead(messageId);
            return response.Success ? Ok(response.Message) : NotFound(response.Message);
        }
    }
}
