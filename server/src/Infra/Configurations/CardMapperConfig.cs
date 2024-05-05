using AutoMapper;
using Domain.Models.DTOs;
using Domain.Models.Entities;

namespace Infra.Configurations;

public class CardMapperConfig : Profile
{
    public CardMapperConfig()
    {
        CreateMap<Card, GetCardDto>()
            .ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.CardType));

        CreateMap<CreateCardDto, Card>()
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.ActiveCard, opt => opt.MapFrom(src => src.ActiveCard))
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId))
            .ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.CardType))
            .ReverseMap();

        CreateMap<DebitCard, GetCardDto>()
            .IncludeBase<Card, GetCardDto>()
            .ForMember(dest => dest.DailyLimit, opt => opt.MapFrom(src => src.DailyLimit));

        CreateMap<CreditCard, GetCardDto>()
            .IncludeBase<Card, GetCardDto>()
            .ForMember(dest => dest.CreditLimit, opt => opt.MapFrom(src => src.CreditLimit));

        CreateMap<DebitCard, CreateCardDto>().ReverseMap();
            
        CreateMap<CreditCard, CreateCardDto>().ReverseMap();

        CreateMap<CreditCard, GetCreditCardDto>().ReverseMap();
    }
}