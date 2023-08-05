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


    public ApiResponse<List<ApartmentResponse>> GetAll()
    {
        try
        {
            var entityList = _unitOfWork.ApartmentRepository.GetAllWithInclude("ApartmentUsers.User", "Building.Block");
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
            var entityList = _unitOfWork.ApartmentRepository.GetByIdWithInclude(id, "ApartmentUsers.User", "Building.Block");
            var mapped = _mapper.Map<Apartment, ApartmentResponse>(entityList);
            return new ApiResponse<ApartmentResponse>(mapped);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "TransactionService.GetById");
            return new ApiResponse<ApartmentResponse>(ex.Message);
        }
    }
}
