using Domain.Models.DTO.ContaDTO;
using Domain.Models.Entities;

namespace Infra.Repositories.ContaRepo;

public interface IContaRepository
{
    Task<List<Conta>> GetAllAsync();
    Task<Conta?> GetByIdAsync(int id);
    Task<Conta> CreateAsync(Conta novaConta);
    Task UpdateAsync(int id, UpdateContaDto updateContaDto);
    Task DeleteAsync(int id);
}