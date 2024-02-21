using Domain.Models.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.ApoliceRepo;

public class ApoliceRepository : IApoliceRepository
{
    private readonly AppDbContext _context;
    
    public ApoliceRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Apolice>> GetAllAsync()
    {
        return await _context.Apolices.ToListAsync();
    }

    public async Task<Apolice?> GetByIdAsync(int id)
    {
        return await _context.Apolices.FindAsync(id);
    }

    public async Task<Apolice> CreateAsync(Apolice novaApolice)
    {
        _context.Apolices.Add(novaApolice);
        await _context.SaveChangesAsync();

        return novaApolice;
    }

    public async Task UpdateAsync(int id, Apolice apolice)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        Apolice apolice = await GetByIdAsync(id);

        if (apolice != null)
        {
            _context.Apolices.Remove(apolice);

            await _context.SaveChangesAsync();
        }
    }
}