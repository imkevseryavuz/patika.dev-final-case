using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Business;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Service.Controller;

[Route("panel/api/[controller]")]
[ApiController]
[Authorize(Roles ="Admin")]
public class ApartmentController : ControllerBase
{
    private readonly IApartmentService service;

    public ApartmentController(IApartmentService service)
    {
        this.service = service;
    }
    [HttpGet]
    public ApiResponse<List<ApartmentResponse>> GetAll()
    {
        var response = service.GetAll();
        return response;
    }

    [HttpPost]
    public ApiResponse Post([FromBody] ApartmentRequest request)
    {
        var response = service.Insert(request);
        return response;
    }
    [HttpGet("{id}")]
    public ApiResponse<ApartmentResponse> Get(int id)
    {
        var response = service.GetById(id);
        return response;
    }
    [HttpPut("{id}")]
    public ApiResponse Put(int id, [FromBody] ApartmentRequest request)
    {

        var response = service.Update(id, request);
        return response;
    }


    [HttpDelete("{id}")]
    public ApiResponse Delete(int id)
    {
        var response = service.Delete(id);
        return response;
    }

}
