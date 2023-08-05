
using AutoMapper;
using Serilog;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Business.Generic;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Data.Uow;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Business;

public class BillService:GenericService<Bill, BillRequest, BillResponse>,IBillService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public BillService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public ApiResponse<List<BillResponse>> GetAll()
    {
        try
        {
            var entityList = _unitOfWork.BillRepository.GetAllWithInclude("ApartmentUser.Apartment");
            var mapped = _mapper.Map<List<Bill>, List<BillResponse>>(entityList);
            return new ApiResponse<List<BillResponse>>(mapped);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "BillService.GetAll");
            return new ApiResponse<List<BillResponse>>(ex.Message);
        }
    }

    public ApiResponse<BillResponse> GetById(int id)
    {
        throw new NotImplementedException();
    }
}
