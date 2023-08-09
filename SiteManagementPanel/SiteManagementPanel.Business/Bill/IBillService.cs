using SiteManagamentPanel.Base;
using SiteManagementPanel.Business.Generic;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Business;

public interface IBillService:IGenericService<Bill,BillRequest, BillResponse>
{
    ApiResponse<List<BillResponse>> GetAll();
    ApiResponse<BillResponse> GetById(int id);
    ApiResponse<List<PaidBillResponse>> GetPaidBills();
    ApiResponse UpdateBillPaymentId(UpdateBillRequest request, int billId, int paymentId);
    ApiResponse InsertBill(BillRequest request);
}
