using AutoMapper;
using Serilog;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Business.Generic;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Data.Uow;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Business;

public class ApartmentService : GenericService<Apartment, ApartmentRequest, ApartmentResponse>, IApartmentService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ApartmentService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public ApiResponse<List<ApartmentResponse>> GetAllApartment()
    {
        try
        {
            var entityList = _unitOfWork.ApartmentRepository.GetAllWithInclude("ApartmentUsers.User", "Building.Block", "ApartmentType");
            var mapped = _mapper.Map<List<Apartment>, List<ApartmentResponse>>(entityList);
            return new ApiResponse<List<ApartmentResponse>>(mapped);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "ApartmentService.GetAll");
            return new ApiResponse<List<ApartmentResponse>>(ex.Message);
        }
    }

    public ApiResponse<ApartmentResponse> GetById(int id)
    {
        try
        {
            var entityList = _unitOfWork.ApartmentRepository.GetByIdWithInclude(id, "ApartmentUsers.User", "Building.Block", "ApartmentType");
            var mapped = _mapper.Map<Apartment, ApartmentResponse>(entityList);
            return new ApiResponse<ApartmentResponse>(mapped);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "ApartmentService.GetById");
            return new ApiResponse<ApartmentResponse>(ex.Message);
        }
    }

    public ApiResponse InsertApartment(ApartmentRequest request)
    {
        try
        {
            var newApartment = _mapper.Map<Apartment>(request);
            _unitOfWork.ApartmentRepository.Insert(newApartment);
            _unitOfWork.Complete();


            var userEntity = _unitOfWork.UserRepository.GetById(request.UserId);

            if (userEntity != null)
            {
                var apartmentUser = new ApartmentUser
                {
                    UserId = userEntity.Id,
                    AparmentId = newApartment.Id

                };

                _unitOfWork.ApartmentUserRepository.Insert(apartmentUser);
                _unitOfWork.Complete();

                return new ApiResponse(message: "Apartment inserted successfully.");
            }
            else
            {      
                return new ApiResponse(message: "Apartment not found.");
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while inserting an apartment.");
            return new ApiResponse(message: "An error occurred while inserting the apartment. Error Details: " + ex.Message);
        }
    }

    public ApiResponse UpdateApartment(int id, UpdateApartmentRequest request)
    {
        try
        {
            var existingApartment = _unitOfWork.ApartmentRepository.GetById(id);

            if (existingApartment == null)
            {
                return new ApiResponse(message: "Apartment not found.");
            }
            _mapper.Map(request, existingApartment);

            _unitOfWork.ApartmentRepository.Update(existingApartment);
            _unitOfWork.Complete();

            var userEntity = _unitOfWork.UserRepository.GetById(request.UserId);
            var apartmentUser = new ApartmentUser
            {
                UserId = userEntity.Id,
                AparmentId = existingApartment.Id

            };

            _unitOfWork.ApartmentUserRepository.Insert(apartmentUser);
            _unitOfWork.Complete();

            return new ApiResponse(message: "Apartment updated successfully.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while updating an apartment.");

            return new ApiResponse(message: "An error occurred while updating the apartment. Error Details: " + ex.Message);
        }
    }

    public ApiResponse DeleteApartment(int apartmentId)
    {
        try
        {
            var apartmentToDelete = _unitOfWork.ApartmentRepository.GetById(apartmentId);
            if (apartmentToDelete == null)
            {
                return new ApiResponse(message: "Apartment not found.");
            }

            _unitOfWork.ApartmentRepository.Delete(apartmentToDelete);
            _unitOfWork.Complete();


            var apartmentUsersToDelete = _unitOfWork.ApartmentUserRepository.Where(au => au.AparmentId == apartmentId);

            foreach (var apartmentUser in apartmentUsersToDelete)
            {
                _unitOfWork.ApartmentUserRepository.Delete(apartmentUser);
            }
            _unitOfWork.Complete();

            return new ApiResponse(message: "Apartment deleted successfully.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while deleting an apartment.");

            return new ApiResponse(message: "An error occurred while deleting the apartment. Error Details: " + ex.Message);
        }
    }
}


