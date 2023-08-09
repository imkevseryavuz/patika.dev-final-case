using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Business;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Service.Controller;

[Route("panel/api/[controller]")]
[ApiController]
[Authorize(Roles ="Admin")]
public class BillController : ControllerBase
{
    private readonly IBillService _billService;

    public BillController(IBillService billService)
    {
        _billService = billService;
    }
    [HttpGet]
    public ApiResponse<List<BillResponse>> GetAll()
    {
        var response = _billService.GetAll();
        return response;
    }
    [HttpGet("paid")]
    public ApiResponse<List<PaidBillResponse>> GetPaidBills()
    {
        var response = _billService.GetPaidBills();
        return response;
    }
    [HttpPost]
    public ApiResponse Post([FromBody] BillRequest request)
    {
        var response = _billService.InsertBill(request);
        return response;
    }
    [HttpGet("{id}")]
    public ApiResponse<BillResponse> Get(int id)
    {
        var response = _billService.GetById(id);
        return response;
    }
}
