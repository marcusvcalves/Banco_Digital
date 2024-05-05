using Infra.Repositories.Interfaces;
using Infra.Data;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.Implementations;

public class CardRepository : ICardRepository
{
    private readonly AppDbContext _context;
    
    public CardRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Card>> GetAllAsync()
    {
        return await _context.Cards.Include(card => card.Account).ToListAsync();
    }
    public async Task<Card?> GetByIdAsync(int id)
    {
        return await _context.Cards.FindAsync(id);
    }
    
    public async Task<T> CreateAsync<T>(T newCard) where T : Card
    {
        _context.Cards.Add(newCard);
        await _context.SaveChangesAsync();

        return newCard;
    }
    public async Task UpdateAsync(Card card)
    {
        _context.Entry(card).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Card card)
    {
        _context.Cards.Remove(card);
        await _context.SaveChangesAsync();
    }
}