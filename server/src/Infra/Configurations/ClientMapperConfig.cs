using AutoMapper;
using Domain.Models.DTOs;
using Domain.Models.Entities;

namespace Infra.Configurations;

public class ClientMapperConfig : Profile
{
    public ClientMapperConfig()
    {
        CreateMap<Client, GetClientDto>()
            .ForMember(dest => dest.ClientType, opt => opt.MapFrom(src => src.ClientTypeString))
            .ReverseMap();
        CreateMap<CreateClientDto, Client>().ReverseMap();
    }
}