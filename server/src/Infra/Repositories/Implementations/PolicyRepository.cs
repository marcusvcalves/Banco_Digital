using Infra.Repositories.Interfaces;
using Domain.Models.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.Implementations;

public class PolicyRepository : IPolicyRepository
{
    private readonly AppDbContext _context;
    
    public PolicyRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Policy>> GetAllAsync()
    {
        return await _context.Policies.Include(policy => policy.CreditCard).ToListAsync();
    }

    public async Task<Policy?> GetByIdAsync(int id)
    {
        return await _context.Policies.FindAsync(id);
    }

    public async Task<Policy> CreateAsync(Policy newPolicy)
    {
        _context.Policies.Add(newPolicy);
        await _context.SaveChangesAsync();

        return newPolicy;
    }

    public async Task UpdateAsync(Policy policy)
    {
        _context.Entry(policy).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Policy policy)
    {
        _context.Policies.Remove(policy);
        await _context.SaveChangesAsync();
    }
}