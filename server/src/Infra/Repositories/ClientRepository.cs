using Domain.Interfaces;
using Domain.Models.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;
    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Client>> GetAllAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task<Client> CreateAsync(Client newClient)
    {
        _context.Clients.Add(newClient);
        await _context.SaveChangesAsync();

        return newClient;
    }

    public async Task UpdateAsync(int id, Client client)
    {
        Client? existingClient = await GetByIdAsync(id);
        
        if (existingClient != null)
        {
            existingClient.Cpf = client.Cpf;
            existingClient.Name = client.Name;
            existingClient.BirthDate = client.BirthDate;
            existingClient.Address = client.Address;
                
            _context.Entry(existingClient).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }


    public async Task DeleteAsync(int id)
    {
        Client? client = await GetByIdAsync(id);

        if (client != null)
        {
            _context.Clients.Remove(client);

            await _context.SaveChangesAsync();
        }
    }
}