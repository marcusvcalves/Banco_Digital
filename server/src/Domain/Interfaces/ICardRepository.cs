using Domain.Models.Entities;

namespace Domain.Interfaces;

public interface ICardRepository
{
    Task<List<Card>> GetAllAsync();
    Task<Card?> GetByIdAsync(int id);
    Task<T> CreateAsync<T>(T newCard) where T : Card;
    Task UpdateAsync(Card card);
    Task DeleteAsync(Card card);
}