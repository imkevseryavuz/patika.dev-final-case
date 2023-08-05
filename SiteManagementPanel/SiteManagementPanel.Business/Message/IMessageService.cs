

using SiteManagamentPanel.Base;
using SiteManagementPanel.Business.Generic;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Business;

public interface IMessageService
{
    ApiResponse<MessageResponse> SendMessage(MessageRequest messageRequest);
    ApiResponse<List<MessageResponse>> GetMessagesByUserId(int userId);
    ApiResponse<MessageResponse> GetMessageById(int messageId);
    ApiResponse MarkMessageAsRead(int messageId);
}
