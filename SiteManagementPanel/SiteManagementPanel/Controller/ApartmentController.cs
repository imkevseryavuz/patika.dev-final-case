using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SiteManagamentPanel.Base;
using SiteManagementPanel.Business;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Service.Controller;

[Route("panel/api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class ApartmentController : ControllerBase
{
    private readonly IApartmentService service;

    public ApartmentController(IApartmentService service)
    {
        this.service = service;
    }
    [HttpGet]
    public ApiResponse<List<ApartmentResponse>> GetAllApartment()
    {
        var response = service.GetAllApartment();
        return response;
    }

    [HttpPost]
    public ApiResponse Post([FromBody] ApartmentRequest request)
    {
        try
        {
            var response = service.InsertApartment(request);

            return response;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while inserting an apartment.");
            return new ApiResponse(message: "An error occurred while inserting the apartment.");
        }
    }

    [HttpGet("{id}")]
    public ApiResponse<ApartmentResponse> Get(int id)
    {
        var response = service.GetById(id);
        return response;
    }

    [HttpPut("{id}")]
    public ApiResponse Put(int id, [FromBody] UpdateApartmentRequest request)
    {
        var response = service.UpdateApartment(id, request);
        return response;
    }

    [HttpDelete("{id}")]
    public ApiResponse Delete(int id)
    {
        var response = service.DeleteApartment(id);
        return response;
    }

}
