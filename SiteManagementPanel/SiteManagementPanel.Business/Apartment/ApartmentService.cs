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
            // ApartmentRequest modelini Apartment nesnesine dönüştür
            var newApartment = _mapper.Map<Apartment>(request);
            _unitOfWork.ApartmentRepository.Insert(newApartment);
            _unitOfWork.Complete();


            var userEntity = _unitOfWork.UserRepository.GetById(request.UserId);

            if (userEntity != null)
            {
                // Yeni ApartmentUser nesnesini oluşturur ve ilgili alanları ayarlar
                var apartmentUser = new ApartmentUser
                {
                    UserId = userEntity.Id,
                    AparmentId=newApartment.Id
                    
                };

                // Daha sonra ApartmentUser'ı veritabanına ekliyor
                _unitOfWork.ApartmentUserRepository.Insert(apartmentUser);
                _unitOfWork.Complete();

                return new ApiResponse(message: "Apartment inserted successfully.");
            }
            else
            {
                // Kullanıcı bulunamadı hatası döndürün
                return new ApiResponse(message: "Apartment not found.");
            }
        }
        catch (Exception ex)
        {
            // Hata mesajını daha detaylı bir şekilde kaydetmek için hatayı loglayın
            Log.Error(ex, "Error while inserting an apartment.");

            // Daha detaylı bir hata mesajı döndürün
            return new ApiResponse(message: "An error occurred while inserting the apartment. Error Details: " + ex.Message);
        }
    }

    public ApiResponse UpdateApartment(int id, ApartmentRequest request)
    {
        try
        {
            // ApartmentRequest modelini Apartment nesnesine dönüştür
            var existingApartment = _unitOfWork.ApartmentRepository.GetById(id);

            if (existingApartment == null)
            {
                return new ApiResponse(message: "Apartment not found.");
            }

            // Update existingApartment properties with the values from the request
            _mapper.Map(request, existingApartment);

            _unitOfWork.ApartmentRepository.Update(existingApartment);
            _unitOfWork.Complete();

            return new ApiResponse(message: "Apartment updated successfully.");
        }
        catch (Exception ex)
        {
            // Hata mesajını daha detaylı bir şekilde kaydetmek için hatayı loglayın
            Log.Error(ex, "Error while updating an apartment.");

            // Daha detaylı bir hata mesajı döndürün
            return new ApiResponse(message: "An error occurred while updating the apartment. Error Details: " + ex.Message);
        }
    }
}


