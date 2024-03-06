using Domain.Models.Entities;

namespace Infra.Repositories.ContaRepo;

public interface IContaRepository
{
    Task<List<Conta>> GetAllAsync();
    Task<Conta?> GetByIdAsync(int id);
    Task<Conta> CreateAsync(Conta novaConta);
    Task TransferAsync(int idContaEnvio, int idContaRecebedora, decimal quantia);
    Task UpdateAsync(int id, Conta conta);
    Task DeleteAsync(int id);
}