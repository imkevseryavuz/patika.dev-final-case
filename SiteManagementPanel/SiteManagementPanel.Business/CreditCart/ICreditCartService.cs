using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Business;
public interface ICreditCartService
{
    CreditCartResponse ProcessPayment(CreditCardRequest request);
}

