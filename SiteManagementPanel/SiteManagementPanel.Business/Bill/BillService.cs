using AutoMapper;
using Serilog;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Business.Generic;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Data.Uow;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Business;

public class BillService : GenericService<Bill, BillRequest, BillResponse>, IBillService
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
            var entityList = _unitOfWork.BillRepository.GetAllWithInclude("ApartmentUser.Apartment", "BillType");
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
        try
        {
            var entityList = _unitOfWork.BillRepository.GetByIdWithInclude(id, "ApartmentUsers.User");
            var mapped = _mapper.Map<Bill, BillResponse>(entityList);
            return new ApiResponse<BillResponse>(mapped);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "BillService.GetById");
            return new ApiResponse<BillResponse>(ex.Message);
        }
    }
    public ApiResponse<List<PaidBillResponse>> GetPaidBills()
    {
        try
        {
            var paidBills = _unitOfWork.BillRepository.GetAllWithInclude("ApartmentUser.Apartment", "BillType","Payment")
                                                      .Where(bill => bill.PaymentId != null)
                                                      .ToList();

            var paidBillResponses = _mapper.Map<List<PaidBillResponse>>(paidBills);
            return new ApiResponse<List<PaidBillResponse>>(paidBillResponses);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "BillService.GetPaidBills");
            return new ApiResponse<List<PaidBillResponse>>(ex.Message);
        }
    }

    public ApiResponse InsertBill(BillRequest request)
    {
        try
        {
            // ApartmentRequest modelini Apartment nesnesine dönüştürüyor
            var newBill = _mapper.Map<Bill>(request);

            // ApartmentUser nesnesini veritabanından çekiyor
            var apartmentUser = _unitOfWork.ApartmentUserRepository.GetById(request.ApartmentId);

            // ApartmentUser nesnesini Bill nesnesine ekliyor
            newBill.ApartmentUser = apartmentUser;

            _unitOfWork.BillRepository.Insert(newBill);
            _unitOfWork.Complete();

            return new ApiResponse(message: "Bill inserted successfully.");

        }
        catch (Exception ex)
        {
           
            Log.Error(ex, "Error while inserting a bill.");
            return new ApiResponse(message: "An error occurred while inserting the bill. Error Details: " + ex.Message);
        }
    }

    public ApiResponse UpdateBillPaymentId(UpdateBillRequest request, int billId, int paymentId)
    {
        var bill = _unitOfWork.BillRepository.GetById(billId);
        if (bill == null)
        {
            return new ApiResponse("Bill not found");
        }

        bill.PaymentId = paymentId;
        _unitOfWork.BillRepository.Update(bill);
        _unitOfWork.Complete();

        return new ApiResponse("Bill PaymentId updated successfully");
    }
}
