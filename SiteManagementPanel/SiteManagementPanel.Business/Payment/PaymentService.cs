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
            var entityList = _unitOfWork.PaymentRepository.GetAllWithInclude("ApartmentUser.User", "ApartmentUser.Apartment","Bill.BillType");
            var mapped = _mapper.Map<List<Payment>, List<PaymentResponse>>(entityList);
            return new ApiResponse<List<PaymentResponse>>(mapped);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "PaymentService.GetAll");
            return new ApiResponse<List<PaymentResponse>>(ex.Message);
        }
    }

    public ApiResponse<PaymentRequest> ProcessCreditCardPayment(CreditCardRequest creditCardRequest, int apartmentUserId, int billId)
    {
        var bill = _unitOfWork.BillRepository.GetById(billId);
        if (bill == null)
        {
            return new ApiResponse<PaymentRequest>("Bill not found");
        }
        var user = _unitOfWork.UserRepository.GetById(apartmentUserId);
        if (user == null)
        {
            return new ApiResponse<PaymentRequest>("User not found");
        }

        var payment = new Payment
        {
            BillId = billId,
            ApartmentUserId = apartmentUserId,
            PaymentDate = DateTime.Now
        };
        var paymentRequest = new PaymentRequest
        { 
            ApartmentId=payment.ApartmentUserId,
            Type=payment.BillId.ToString(),
            PaymentDate = payment.PaymentDate
        };

        return new ApiResponse<PaymentRequest>(paymentRequest);
    }

}

