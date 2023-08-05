using SiteManagamentPanel.Base;
using SiteManagementPanel.Business.Generic;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Business;

public interface IUserLogService : IGenericService<UserLog, UserLogRequest, UserLogResponse>
{
    ApiResponse<List<UserLogResponse>> GetByUserSession(string username);
}
