using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
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
            try
            {
                var response = _messageService.SendMessage(messageRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse("An error occurred while sending message. Error details: " + ex.Message);
                return BadRequest(errorResponse);
            }
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
            try
            {
                var response = _messageService.GetMessageById(messageId);
                return Ok(response);
            }
            catch (Exception ex)
            {

                Log.Error(ex, "An error occurred while marking message as read.");
                var errorResponse = new ApiResponse("An error occurred while getting message. Error details: " + ex.Message);
                return BadRequest(errorResponse);
            }

        }

        [HttpPost("{messageId}/mark-as-read")]
        public IActionResult MarkMessageAsRead(int messageId)
        {
            try
            {
                var response = _messageService.MarkMessageAsRead(messageId);
                return Ok(response);
            }
            catch (Exception ex)
            {

                Log.Error(ex, "An error occurred while marking message as read.");
                var errorResponse = new ApiResponse("An error occurred while marking message as read. Error details: " + ex.Message);
                return BadRequest(errorResponse);
            }
        }
    }
}
