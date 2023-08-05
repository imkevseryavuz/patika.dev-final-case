using AutoMapper;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Business.Generic;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Schema;


namespace SiteManagementPanel.Business;

public interface IPaymentService
{
    public ApiResponse<PaymentRequest> ProcessCreditCardPayment(CreditCardRequest creditCardRequest, int apartmentUserId, int billId);
    ApiResponse<List<PaymentResponse>> GetAll();
   
}
