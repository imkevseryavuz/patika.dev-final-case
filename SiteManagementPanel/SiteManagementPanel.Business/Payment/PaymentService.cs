using AutoMapper;
using Serilog;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Data.Uow;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Business;

public class PaymentService:IPaymentService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBillService _billService;

    public PaymentService(IMapper mapper, IUnitOfWork unitOfWork, IBillService billService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _billService = billService;
    }
    public ApiResponse<List<PaymentResponse>> GetAll()
    {
        try
        {
            var entityList = _unitOfWork.PaymentRepository.GetAllWithInclude("ApartmentUser.User", "ApartmentUser.Apartment", "Bill.BillType");

            var mapped = _mapper.Map<List<PaymentResponse>>(entityList);

            return new ApiResponse<List<PaymentResponse>>(mapped);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "PaymentService.GetAll");
            return new ApiResponse<List<PaymentResponse>>(ex.Message);
        }
    }
    public ApiResponse ProcessCreditCardPayment(CreditCardRequest creditCardRequest, int apartmentUserId, int billId)
    {
        var bill = _unitOfWork.BillRepository.GetById(billId);
        if (bill == null)
        {
            return new ApiResponse("Bill not found");
        }
        var user = _unitOfWork.UserRepository.GetById(apartmentUserId);
        if (user == null)
        {
            return new ApiResponse("User not found");
        }

        var paymentEntity = _mapper.Map<CreditCardRequest, Payment>(creditCardRequest);
        paymentEntity.PaymentDate = DateTime.UtcNow;
        _unitOfWork.PaymentRepository.Insert(paymentEntity);
        _unitOfWork.Complete();


        UpdateBillRequest updateBillRequest = new UpdateBillRequest
        {
            PaymentId = paymentEntity.Id
        };

        ApiResponse updateResponse = _billService.UpdateBillPaymentId(updateBillRequest, billId, paymentEntity.Id);

        _unitOfWork.Complete();
        return new ApiResponse("Payment processed successfully");

    }

}

