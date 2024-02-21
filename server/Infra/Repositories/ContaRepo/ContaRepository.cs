using Domain.Models.DTO.ContaDTO;
using Domain.Models.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.ContaRepo;

public class ContaRepository : IContaRepository
{
    private readonly AppDbContext _context;
    
    public ContaRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Conta>> GetAllAsync()
    {
        return await _context.Contas.ToListAsync();
    }

    public async Task<Conta?> GetByIdAsync(int id)
    {
        return await _context.Contas.FindAsync(id);
    }

    public async Task<Conta> CreateAsync(Conta novaConta)
    {
        _context.Contas.Add(novaConta);
        await _context.SaveChangesAsync();

        return novaConta;
    }

    public async Task UpdateAsync(int id, UpdateContaDto updateContaDto)
    {
        Conta contaExistente = await GetByIdAsync(id);

        if (contaExistente != null)
        {
            contaExistente.Senha = updateContaDto.Senha;
            
            _context.Entry(contaExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        Conta conta = await GetByIdAsync(id);

        if (conta != null)
        {
            _context.Contas.Remove(conta);

            await _context.SaveChangesAsync();
        }
    }
}