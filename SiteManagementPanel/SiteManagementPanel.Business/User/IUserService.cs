using SiteManagamentPanel.Base;
using SiteManagementPanel.Business.Generic;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Schema;


namespace SiteManagementPanel.Business;

public interface IUserService : IGenericService<User, UserRequest, UserResponse>
{
    ApiResponse<UserResponse> GetById(int id);
    ApiResponse UpdateUser(int id,UserRequest request);
    ApiResponse DeleteUser(int id);

}