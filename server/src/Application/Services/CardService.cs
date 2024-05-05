using AutoMapper;
using Infra.Repositories.Interfaces;
using Domain.Models.DTOs;
using Domain.Models.Entities;
using Domain.Models.Enums;

namespace Application.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public CardService(ICardRepository cardRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetCardDto>> GetAllCardsAsync()
        {
            List<Card> cards = await _cardRepository.GetAllAsync();
            return cards.Select(card => _mapper.Map<GetCardDto>(card));
        }

        public async Task<GetCardDto> GetCardByIdAsync(int id)
        {
            Card? card = await _cardRepository.GetByIdAsync(id);
            return _mapper.Map<GetCardDto>(card);
        }

        public async Task<GetCardDto> CreateCardAsync(CreateCardDto createCardDto)
        {
            Account? existingAccount = await _accountRepository.GetByIdAsync(createCardDto.AccountId);

            if (existingAccount == null)
                throw new ArgumentException("A conta correspondente não pode ser encontrada.");

            Card newCard;

            switch (createCardDto.CardType)
            {
                case CardType.Debit:
                    newCard = _mapper.Map<DebitCard>(createCardDto);
                    break;
                case CardType.Credit:
                    newCard = _mapper.Map<CreditCard>(createCardDto);
                    break;
                default:
                    throw new ArgumentException("Tipo de cartão inválido.");
            }
            
            await _cardRepository.CreateAsync(newCard);
    
            return _mapper.Map<GetCardDto>(newCard);
        }

        public async Task<GetCardDto> UpdateCardAsync(int id, Card card)
        {
            Card? existingCard = await _cardRepository.GetByIdAsync(id);

            if (existingCard == null)
                throw new ArgumentException("O cartão especificado não existe.");

            existingCard.Number = card.Number;
            existingCard.CardType = card.CardType;
            existingCard.ActiveCard = card.ActiveCard;

            await _cardRepository.UpdateAsync(existingCard);
            return _mapper.Map<GetCardDto>(existingCard);
        }

        public async Task DeleteCardAsync(int id)
        {
            Card? cardToDelete = await _cardRepository.GetByIdAsync(id);

            if (cardToDelete == null)
                throw new ArgumentException("O cartão especificado não existe.");

            await _cardRepository.DeleteAsync(cardToDelete);
        }
    }
}
