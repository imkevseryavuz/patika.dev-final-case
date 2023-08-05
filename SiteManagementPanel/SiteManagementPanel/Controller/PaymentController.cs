using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Business;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Service.Controller
{
    [Route("panel/api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public ApiResponse<List<PaymentResponse>> GetAll()
        {
            var response = _paymentService.GetAll();
            return response;
        }

        [HttpPost("process-credit-card-payment")]
        public IActionResult ProcessCreditCardPayment([FromBody] CreditCardRequest creditCardRequest, int apartmentUserId, int billId)
        {
            ApiResponse<PaymentRequest> paymentResponse = _paymentService.ProcessCreditCardPayment(creditCardRequest, apartmentUserId, billId);

            if (paymentResponse.Success)
            {
                return Ok(paymentResponse.Response);
            }
            else
            {
                return BadRequest(paymentResponse.Message);
            }
        }
    }
}
