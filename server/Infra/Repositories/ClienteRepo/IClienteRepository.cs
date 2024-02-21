using Domain.Models.DTO.ClienteDTO;
using Domain.Models.Entities;

namespace Infra.Repositories.ClienteRepo;

public interface IClienteRepository
{
    Task<List<Cliente>> GetAllAsync();
    Task<Cliente?> GetByIdAsync(int id);
    Task<Cliente> CreateAsync(Cliente novoCliente);
    Task UpdateAsync(int id, UpdateClienteDto updateClienteDto);
    Task DeleteAsync(int id);
}