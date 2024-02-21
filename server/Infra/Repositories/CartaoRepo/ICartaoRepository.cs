using Domain.Models.DTO.CartaoDTO;
using Domain.Models.Entities;

namespace Infra.Repositories.CartaoRepo;

public interface ICartaoRepository
{
    Task<List<Cartao>> GetAllAsync();
    Task<Cartao?> GetByIdAsync(int id);
    Task<Cartao> CreateAsync(Cartao novoCartao);
    Task UpdateAsync(int id, UpdateCartaoDto updateCartaoDto);
    Task DeleteAsync(int id);
}