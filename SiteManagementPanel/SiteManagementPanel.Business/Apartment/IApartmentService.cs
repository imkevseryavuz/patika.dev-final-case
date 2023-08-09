using SiteManagamentPanel.Base;
using SiteManagementPanel.Business.Generic;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Schema;
using System.Security.Claims;

namespace SiteManagementPanel.Business;

public interface IApartmentService:IGenericService<Apartment,ApartmentRequest,ApartmentResponse>
{
   ApiResponse<List<ApartmentResponse>> GetAllApartment();
    ApiResponse<ApartmentResponse> GetById(int id);
    ApiResponse InsertApartment(ApartmentRequest request);
    ApiResponse UpdateApartment(int id, ApartmentRequest request);

    
}
