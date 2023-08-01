
using AutoMapper;
using SiteManagement.Data;
using SiteManagementPanel.Domain;

namespace SiteManagementPanel.Schema;

public class MapperConfig:Profile
{
    public MapperConfig()
    {
        //Apartman
        CreateMap<ApartmentRequest, Apartment>();
        CreateMap<Apartment, ApartmentResponse>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
            .ForMember(dest => dest.BlockName, opt => opt.MapFrom(src => src.Block.BlockName))
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.TypeName))
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.StatusName));

        //Ortak Giderler
        CreateMap<BillRequest, Bill>();
        CreateMap<Bill, BillResponse>()
            .ForMember(dest => dest.ApartmentNumber, opt => opt.MapFrom(src => src.Apartment.ApartmentNumber));

        //Ödemeler
        CreateMap<PaymentRequest, Payment>();
        CreateMap<Payment, PaymentResponse>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
            .ForMember(dest => dest.ApartmentNumber, opt => opt.MapFrom(src => src.Apartment.ApartmentNumber));


        //User Girişi
        CreateMap<UserLogRequest, UserLog>();
        CreateMap<UserLog, UserLogResponse>();

        //User Ekleme
        CreateMap<UserRequest, User>();
        CreateMap<User, UserResponse>();
    }
}
