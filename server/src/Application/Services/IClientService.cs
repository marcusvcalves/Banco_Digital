using Domain.Models.DTOs;
using Domain.Models.Entities;

namespace Application.Services;

public interface IClientService
{
    Task<IEnumerable<GetClientDto>> GetAllClientsAsync();
    Task<GetClientDto> GetClientByIdAsync(int id);
    Task<GetClientDto> CreateClientAsync(CreateClientDto createClientDto);
    Task<GetClientDto> UpdateClientAsync(int id, Client client);
    Task DeleteClientAsync(int id);
}