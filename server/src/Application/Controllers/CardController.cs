using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.Models.DTOs;
using Domain.Models.Entities;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/v1/cartoes")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        /// <summary>
        /// Retorna todos os cartões.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<GetCardDto> cards = await _cardService.GetAllCardsAsync();
            return Ok(cards);
        }

        /// <summary>
        /// Retorna o cartão com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cartão a ser recuperado.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCardDto>> GetById([FromRoute] int id)
        {
            GetCardDto? card = await _cardService.GetCardByIdAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return Ok(card);
        }

        /// <summary>
        /// Cria um novo cartão.
        /// </summary>
        /// <param name="createCardDto">Os dados do novo cartão a ser criado.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCardDto createCardDto)
        {
            try
            {
                GetCardDto newCard = await _cardService.CreateCardAsync(createCardDto);
                return CreatedAtAction(nameof(GetById), new { id = newCard.Id }, newCard);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar o cartão: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza o cartão com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cartão a ser atualizado.</param>
        /// <param name="card">Os novos dados do cartão.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Card card)
        {
            try
            {
                var updatedCard = await _cardService.UpdateCardAsync(id, card);
                return Ok(updatedCard);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar o cartão: {ex.Message}");
            }
        }

        /// <summary>
        /// Deleta o cartão com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cartão a ser deletado.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                bool result = await _cardService.DeleteCardAsync(id);
                
                if (result)
                    return NoContent();
                
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar o cartão: {ex.Message}");
            }
        }
    }
}
