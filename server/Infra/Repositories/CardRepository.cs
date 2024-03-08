using Domain.Interfaces;
using Infra.Data;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class CardRepository : ICardRepository
{
    private readonly AppDbContext _context;
    
    public CardRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Card>> GetAllAsync()
    {
        return await _context.Cards.ToListAsync();
    }
    public async Task<Card?> GetByIdAsync(int id)
    {
        return await _context.Cards.FindAsync(id);
    }
    public async Task<Card> CreateAsync(Card newCard)
    {
        _context.Cards.Add(newCard);
        await _context.SaveChangesAsync();

        return newCard;
    }
    public async Task UpdateAsync(int id, Card card)
    {
        Card? existingCard = await GetByIdAsync(id);

        if (existingCard != null)
        {
            existingCard.ActiveCard = card.ActiveCard;
            existingCard.Password = card.Password;
            
            _context.Entry(existingCard).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
    }
    public async Task DeleteAsync(int id)
    {
        Card? card = await GetByIdAsync(id);

        if (card != null)
        {
            _context.Cards.Remove(card);

            await _context.SaveChangesAsync();
        }
    }
}