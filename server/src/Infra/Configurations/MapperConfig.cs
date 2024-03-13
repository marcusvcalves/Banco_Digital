using AutoMapper;
using Domain.Models.DTOs;
using Domain.Models.Entities;

namespace Infra.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // policies
            CreateMap<Policy, GetPolicyDto>().ReverseMap();
            CreateMap<Policy, CreatePolicyDto>().ReverseMap();
            
            // clients
            CreateMap<Client, GetClientDto>()
                .ForMember(dest => dest.ClientType, opt => opt.MapFrom(src => src.ClientTypeString))
                .ReverseMap();
            CreateMap<CreateClientDto, Client>().ReverseMap();
            
            // accounts
            CreateMap<Account, GetAccountDto>()
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
                .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src =>
                    (src is CheckingAccount) ? "checking" :
                    (src is SavingsAccount) ? "savings" :
                    null));
                    
            CreateMap<CreateAccountDto, Account>()
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
                .ForMember(dest => dest.Client, opt => opt.Ignore())
                .ForMember(dest => dest.Cards, opt => opt.Ignore());

            CreateMap<CreateAccountDto, CheckingAccount>()
                .IncludeBase<CreateAccountDto, Account>()
                .ForMember(dest => dest.MonthlyFee, opt => opt.MapFrom(_ => 2));

            CreateMap<CreateAccountDto, SavingsAccount>()
                .IncludeBase<CreateAccountDto, Account>()
                .ForMember(dest => dest.ReturnRates, opt => opt.MapFrom(_ => 0.5m));

            // cards
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
}