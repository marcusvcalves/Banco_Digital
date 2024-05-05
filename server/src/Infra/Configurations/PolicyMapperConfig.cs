using AutoMapper;
using Domain.Models.DTOs;
using Domain.Models.Entities;

namespace Infra.Configurations;

public class PolicyMapperConfig : Profile
{
    public PolicyMapperConfig()
    {
        CreateMap<Policy, GetPolicyDto>().ReverseMap();
        CreateMap<Policy, CreatePolicyDto>().ReverseMap();
    }
}