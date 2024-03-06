using Domain.Models.Entities;

namespace Domain.Interfaces;

public interface ICartaoRepository
{
    Task<List<Cartao>> GetAllAsync();
    Task<Cartao?> GetByIdAsync(int id);
    Task<Cartao> CreateAsync(Cartao novoCartao);
    Task UpdateAsync(int id, Cartao cartao);
    Task DeleteAsync(int id);
}