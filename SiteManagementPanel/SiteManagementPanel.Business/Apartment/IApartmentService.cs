using SiteManagamentPanel.Base;
using SiteManagementPanel.Business.Generic;
using SiteManagementPanel.Data.Domain;
using SiteManagementPanel.Schema;

namespace SiteManagementPanel.Business;

public interface IApartmentService : IGenericService <Apartment, ApartmentRequest, ApartmentResponse>
{
    ApiResponse<List<ApartmentResponse>> GetAll();
    ApiResponse<ApartmentResponse> GetById(int id);
}
