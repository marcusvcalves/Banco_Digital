using AutoMapper;
using Domain.Models.DTO.CartaoDTO;
using Domain.Models.DTO.ClienteDTO;
using Domain.Models.Entities;

namespace Infra.Configurations;

public class MapperConfig : Profile
{
    public static void Configure()
    {
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Cartao, GetCartaoDto>();
            cfg.CreateMap<Cliente, UpdateClienteDto>();
            cfg.CreateMap<Cliente, CreateClienteDto>();
            cfg.CreateMap<List<Cliente>, List<GetClienteDto>>();
            cfg.CreateMap<Cliente, GetClienteDto>();
        });

        IMapper mapper = config.CreateMapper();
    }
}
