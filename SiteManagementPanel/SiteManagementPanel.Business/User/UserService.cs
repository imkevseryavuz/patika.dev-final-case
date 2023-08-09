using AutoMapper;
using Serilog;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Business.Generic;
using SiteManagementPanel.Data;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Data.Uow;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Business;

public class UserService : GenericService<User, UserRequest, UserResponse>, IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IApartmentService _apartmentService; 
    private readonly IMessageService _messageService;

    public UserService(IMapper mapper, IUnitOfWork unitOfWork, IApartmentService apartmentService,IMessageService messageService) : base(mapper, unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _apartmentService = apartmentService;
        _messageService = messageService;
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
    public ApiResponse UpdateUser(int id, UserRequest request)
    {
        try
        {
            var existingUser = _unitOfWork.UserRepository.GetById(id);
            if (existingUser == null)
            {
                return new ApiResponse(message: "User not found.");
            }
            _mapper.Map(request, existingUser);
            _unitOfWork.UserRepository.Update(existingUser);
            _unitOfWork.Complete();

            return new ApiResponse(message: "User updated successfully.");
        }
        catch (Exception ex)
        {      
            Log.Error(ex, "Error while updating an user.");

            return new ApiResponse(message: "An error occurred while updating the user. Error Details: " + ex.Message);
        }        
    }
    public ApiResponse DeleteUser(int id)
    {
        try
        {            
            var existingUser = _unitOfWork.UserRepository.GetById(id);
            if (existingUser == null)
            {
                return new ApiResponse(message: "User not found.");
            }
            _unitOfWork.MessageRepository.DeleteById(id);
            _unitOfWork.Complete();

            _unitOfWork.UserRepository.Delete(existingUser);
            _unitOfWork.Complete();
        
             _unitOfWork.ApartmentUserRepository.DeleteById(id);
            _unitOfWork.Complete();

            var apartmentToUpdate = _unitOfWork.ApartmentRepository.GetById(id);
            if (apartmentToUpdate != null)
            {
                apartmentToUpdate.Status = ApartmenStatusType.Empty;
                _unitOfWork.Complete();
            }

            return new ApiResponse(message: "User deleted successfully.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while updating an user.");

            return new ApiResponse(message: "An error occurred while updating the user. Error Details: " + ex.Message);
        }
    }
}

