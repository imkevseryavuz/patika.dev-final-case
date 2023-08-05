using AutoMapper;
using Serilog;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Business.Generic;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Data.Uow;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Business;

public class UserService : GenericService<User, UserRequest, UserResponse>, IUserService
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public ApiResponse<UserResponse> GetById(int id)
    {
        try
        {
            var user = _unitOfWork.UserRepository.GetById(id);
            if (user == null)
            {
                return new ApiResponse<UserResponse>("User not found");
            }

            var response = _mapper.Map<User, UserResponse>(user);
            return new ApiResponse<UserResponse>(response);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "UserService.GetUserById");
            return new ApiResponse<UserResponse>(ex.Message);
        }
    }

    public ApiResponse<List<UserResponse>> GetAllUsers()
    {
        try
        {
            var users = _unitOfWork.UserRepository.GetAll().ToList();
            var response = _mapper.Map<List<User>, List<UserResponse>>(users);
            return new ApiResponse<List<UserResponse>>(response);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "UserService.GetAllUsers");
            return new ApiResponse<List<UserResponse>>(ex.Message);
        }
    }
}

