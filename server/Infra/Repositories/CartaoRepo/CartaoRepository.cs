using AutoMapper;
using Domain.Models.DTO.CartaoDTO;
using Infra.Data;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.CartaoRepo;

public class CartaoRepository : ICartaoRepository
{
    private readonly AppDbContext _context;
    
    public CartaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cartao>> GetAllAsync()
    {
        return await _context.Cartoes.ToListAsync();
    }
    public async Task<Cartao?> GetByIdAsync(int id)
    {
        return await _context.Cartoes.FindAsync(id);
    }
    public async Task<Cartao> CreateAsync(Cartao novoCartao)
    {
        _context.Cartoes.Add(novoCartao);
        await _context.SaveChangesAsync();

        return novoCartao;
    }
    public async Task UpdateAsync(int id, Cartao cartao)
    {
        Cartao cartaoExistente = await GetByIdAsync(id);

        if (cartaoExistente != null)
        {
            cartaoExistente.StatusCartao = cartao.StatusCartao;
            cartaoExistente.Senha = cartao.Senha;
            
            _context.Entry(cartaoExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
    }
    public async Task DeleteAsync(int id)
    {
        Cartao cartao = await GetByIdAsync(id);

        if (cartao != null)
        {
            _context.Cartoes.Remove(cartao);

            await _context.SaveChangesAsync();
        }
    }
}