using AutoMapper;
using Domain.Models.DTOs;
using Domain.Models.Entities;

namespace Infra.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Policy, GetPolicyDto>().ReverseMap();
            CreateMap<Card, GetCardDto>().ReverseMap();
            CreateMap<Client, GetClientDto>()
                .ForMember(dest => dest.ClientType, opt => opt.MapFrom(src => src.ClientTypeString));
            CreateMap<Account, GetAccountDto>().ReverseMap();
        }
    }
}