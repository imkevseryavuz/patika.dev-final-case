

using SiteManagementPanel.Business.Generic;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Business;

public interface IMessageService:IGenericService<Message,MessageRequest,MessageResponse>
{

}
