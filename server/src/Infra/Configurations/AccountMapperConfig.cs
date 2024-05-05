using AutoMapper;
using Domain.Models.DTOs;
using Domain.Models.Entities;

namespace Infra.Configurations;

public class AccountMapperConfig : Profile
{
    public AccountMapperConfig()
    {
        CreateMap<Account, GetAccountDto>()
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
            .ReverseMap();
                    
        CreateMap<CreateAccountDto, Account>()
            .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
            .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountType))
            .ForMember(dest => dest.Client, opt => opt.Ignore())
            .ForMember(dest => dest.Cards, opt => opt.Ignore());

        CreateMap<CreateAccountDto, CheckingAccount>()
            .IncludeBase<CreateAccountDto, Account>()
            .ForMember(dest => dest.MonthlyFee, opt => opt.MapFrom(_ => 2));

        CreateMap<CreateAccountDto, SavingsAccount>()
            .IncludeBase<CreateAccountDto, Account>()
            .ForMember(dest => dest.ReturnRates, opt => opt.MapFrom(_ => 0.5m));
    }
}