using Domain.Interfaces;
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
    
    public async Task TransferAsync(int idContaEnvio, int idContaRecebedora, decimal quantia)
    {
        Conta contaOrigem = await GetByIdAsync(idContaEnvio);
        Conta contaRecebedora = await GetByIdAsync(idContaRecebedora);

        if (idContaEnvio != null && idContaRecebedora != null && contaOrigem.Saldo >= quantia)
        {
            contaOrigem.Saldo -= quantia;
            contaRecebedora.Saldo += quantia;

            _context.Entry(contaOrigem).State = EntityState.Modified;
            _context.Entry(contaRecebedora).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Não foi possível concluir a transferência.");
        }
    }

    public async Task UpdateAsync(int id, Conta conta)
    {
        Conta contaExistente = await GetByIdAsync(id);

        if (contaExistente != null)
        {
            contaExistente.Saldo = conta.Saldo;
            contaExistente.Senha = conta.Senha;
            
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