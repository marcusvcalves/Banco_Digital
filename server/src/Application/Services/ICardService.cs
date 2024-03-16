using Domain.Models.DTOs;
using Domain.Models.Entities;

namespace Application.Services
{
    public interface ICardService
    {
        Task<IEnumerable<GetCardDto>> GetAllCardsAsync();
        Task<GetCardDto> GetCardByIdAsync(int id);
        Task<GetCardDto> CreateCardAsync(CreateCardDto createCardDto);
        Task<GetCardDto> UpdateCardAsync(int id, Card card);
        Task DeleteCardAsync(int id);
    }
}