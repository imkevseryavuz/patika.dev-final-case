
using AutoMapper;
using SiteManagementPanel.Data.Domain;

namespace SiteManagementPanel.Schema;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        //Apartman
        CreateMap<ApartmentRequest, Apartment>();
        CreateMap<Apartment, ApartmentResponse>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.ApartmentUsers.Select(p => p.User.FirstName + " " + p.User.LastName).LastOrDefault()))
            .ForMember(dest => dest.BlockName, opt => opt.MapFrom(src => src.Building.Block.BlockName))
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.TypeName))
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.ToString()));

        //Ortak Giderler
        CreateMap<BillRequest, Bill>();
        CreateMap<Bill, BillResponse>()
            .ForMember(dest => dest.ApartmentUserId, opt => opt.MapFrom(src => src.ApartmentUser.Apartment.ApartmentNumber))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.BillType.TypeName))
            .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.Payment.PaymentDate.Date));

        //Ödemeler
        CreateMap<PaymentRequest, Payment>();
        CreateMap<Payment, PaymentResponse>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.ApartmentUser.User.FirstName + " " + src.ApartmentUser.User.LastName))
            .ForMember(dest => dest.ApartmentNumber, opt => opt.MapFrom(src => src.ApartmentUser.Apartment.ApartmentNumber));

        //Mesaj
        CreateMap<MessageRequest, Message>();
        CreateMap<Message, MessageResponse>()
            .ForMember(dest => dest.FromUserName, opt => opt.MapFrom(src => src.FromUser.FirstName))
            .ForMember(dest => dest.ToUserName, opt => opt.MapFrom(src => src.ToUser.FirstName));
        //User Girişi
        CreateMap<UserLogRequest, UserLog>();
        CreateMap<UserLog, UserLogResponse>();

        //User Ekleme
        CreateMap<UserRequest, User>();
        CreateMap<User, UserResponse>();
    }
}
