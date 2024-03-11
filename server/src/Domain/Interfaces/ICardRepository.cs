using Domain.Models.Entities;

namespace Domain.Interfaces;

public interface ICardRepository
{
    Task<List<Card>> GetAllAsync();
    Task<Card?> GetByIdAsync(int id);
    Task<Card> CreateAsync(Card newCard);
    Task UpdateAsync(int id, Card card);
    Task DeleteAsync(int id);
}