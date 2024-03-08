using Domain.Models.Entities;

namespace Domain.Interfaces;

public interface IClientRepository
{
    Task<List<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(int id);
    Task<Client> CreateAsync(Client newClient);
    Task UpdateAsync(int id, Client client);
    Task DeleteAsync(int id);
}