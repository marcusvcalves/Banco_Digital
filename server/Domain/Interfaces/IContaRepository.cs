using Domain.Models.Entities;

namespace Domain.Interfaces;

public interface IContaRepository
{
    Task<List<Conta>> GetAllAsync();
    Task<Conta?> GetByIdAsync(int id);
    Task<Conta> CreateAsync(Conta novaConta);
    Task TransferAsync(int idContaEnvio, int idContaRecebedora, decimal quantia);
    Task UpdateAsync(int id, Conta conta);
    Task DeleteAsync(int id);
}