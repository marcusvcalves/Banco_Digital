using AutoMapper;
using Domain.Interfaces;
using Domain.Models.DTOs;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route(template: "api/v1/cartoes")]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;
        
        public CardController(ICardRepository cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Retorna todos os cartões.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Card> cards = await _cardRepository.GetAllAsync();
            var getCardsDto = cards.Select(card => _mapper.Map<GetCardDto>(card));

            return Ok(getCardsDto);
        }
        
        /// <summary>
        /// Retorna o cartão com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cartão a ser recuperado.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCardDto>> GetById([FromRoute] int id)
        {
            Card? card = await _cardRepository.GetByIdAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            GetCardDto getCardDto = _mapper.Map<GetCardDto>(card);

            return Ok(getCardDto);
        }
        
        /// <summary>
        /// Cria um novo cartão.
        /// </summary>
        /// <param name="card">Os dados do novo cartão a ser criado.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Card card)
        {
            Card newCard = await _cardRepository.CreateAsync(card);
            
            GetCardDto getCardDto = _mapper.Map<GetCardDto>(newCard);

            return CreatedAtAction(nameof(GetById), new { id = newCard.Id }, getCardDto);
        }
        
        /// <summary>
        /// Atualiza o cartão com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cartão a ser atualizado.</param>
        /// <param name="card">Os novos dados do cartão.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Card card)
        {
            Card? existingCard = await _cardRepository.GetByIdAsync(id);
            
            if (existingCard == null)
            {
                return NotFound();
            }
            
            await _cardRepository.UpdateAsync(id, card);
            
            GetCardDto getCardDto = _mapper.Map<GetCardDto>(existingCard);

            return Ok(getCardDto);
        }
        
        /// <summary>
        /// Deleta o cartão com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cartão a ser deletado.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Card? cardToDelete = await _cardRepository.GetByIdAsync(id);

            if (cardToDelete != null)
            {
                await _cardRepository.DeleteAsync(id);

                return NoContent();
            }

            return NotFound();
        }
    }
}
