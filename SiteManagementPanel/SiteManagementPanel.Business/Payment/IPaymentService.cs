using SiteManagamentPanel.Base;
using SiteManagementPanel.Schema;


namespace SiteManagementPanel.Business;

public interface IPaymentService
{
    public ApiResponse ProcessCreditCardPayment(CreditCardRequest creditCardRequest, int apartmentUserId, int billId);
    ApiResponse<List<PaymentResponse>> GetAll();
   
}
