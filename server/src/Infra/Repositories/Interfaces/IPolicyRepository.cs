using Domain.Models.Entities;

namespace Infra.Repositories.Interfaces;
public interface IPolicyRepository
{
    Task<List<Policy>> GetAllAsync();
    Task<Policy?> GetByIdAsync(int id);
    Task<Policy> CreateAsync(Policy newPolicy);
    Task UpdateAsync(Policy policy);
    Task DeleteAsync(Policy policy);
}