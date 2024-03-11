using Domain.Interfaces;
using Domain.Models.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class PolicyRepository : IPolicyRepository
{
    private readonly AppDbContext _context;
    
    public PolicyRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Policy>> GetAllAsync()
    {
        return await _context.Policies.ToListAsync();
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

    public async Task UpdateAsync(int id, Policy policy)
    {
        Policy? existingPolicy = await GetByIdAsync(id);

        if (existingPolicy != null)
        {
            existingPolicy.Number = policy.Number;
            existingPolicy.Value = policy.Value;
            existingPolicy.TriggeringDescription = policy.TriggeringDescription;

            _context.Entry(existingPolicy).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        Policy? policy = await GetByIdAsync(id);

        if (policy != null)
        {
            _context.Policies.Remove(policy);

            await _context.SaveChangesAsync();
        }
    }
}