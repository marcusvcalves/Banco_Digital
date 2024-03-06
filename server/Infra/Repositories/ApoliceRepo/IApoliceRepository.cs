using Domain.Models.Entities;

namespace Infra.Repositories.ApoliceRepo;
public interface IApoliceRepository
{
    Task<List<Apolice>> GetAllAsync();
    Task<Apolice?> GetByIdAsync(int id);
    Task<Apolice> CreateAsync(Apolice novaApolice);
    Task UpdateAsync(int id, Apolice apolice);
    Task DeleteAsync(int id);
}