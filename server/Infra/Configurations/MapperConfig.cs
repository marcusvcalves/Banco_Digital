using AutoMapper;
using Domain.Models.DTO.ApoliceSeguroDTO;
using Domain.Models.DTO.CartaoDTO;
using Domain.Models.DTO.ClienteDTO;
using Domain.Models.DTO.ContaDTO;
using Domain.Models.Entities;

namespace Infra.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Apolice, GetApoliceDto>().ReverseMap();
            CreateMap<Cartao, GetCartaoDto>().ReverseMap();
            CreateMap<Cliente, GetClienteDto>()
                .ForMember(dest => dest.TipoCliente, opt => opt.MapFrom(src => src.TipoClienteString));
            CreateMap<Conta, GetContaDto>().ReverseMap();
        }
    }
}