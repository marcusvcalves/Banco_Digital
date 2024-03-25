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
        /// Retorna todos os cartões
        /// </summary>
        /// <response code="200">Lista com os DTOs de todos os cartões</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<GetCardDto> cards = await _cardService.GetAllCardsAsync();
            return Ok(cards);
        }

        /// <summary>
        /// Retorna o cartão com o ID especificado
        /// </summary>
        /// <param name="id">O ID do cartão a ser recuperado</param>
        /// <response code="200">Retorna o DTO do cartão</response>
        /// <response code="404">Caso não encontre cartão com o ID</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCardDto>> GetById([FromRoute] int id)
        {
            GetCardDto? getCardDto = await _cardService.GetCardByIdAsync(id);

            if (getCardDto == null)
                return NotFound();

            return Ok(getCardDto);
        }

        /// <summary>
        /// Cria um novo cartão.
        /// </summary>
        /// <param name="createCardDto">Os dados do novo cartão a ser criado</param>
        /// <response code="201">Retorna o DTO do cartão</response>
        /// <response code="400">Caso o request estiver incorreto</response>
        /// <response code="500">Erro inesperado</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCardDto createCardDto)
        {
            try
            {
                GetCardDto newCard = await _cardService.CreateCardAsync(createCardDto);
                return CreatedAtAction(nameof(GetById), new { id = newCard.Id }, newCard);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar o cartão: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza o cartão com o ID especificado
        /// </summary>
        /// <param name="id">O ID do cartão a ser atualizado</param>
        /// <param name="card">Os novos dados do cartão</param>
        /// <response code="200">Atualiza o cartão e retorna seu DTO</response>
        /// <response code="404">Caso o request estiver incorreto</response>
        /// <response code="500">Erro inesperado</response>
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
        /// Deleta o cartão com o ID especificado
        /// </summary>
        /// <param name="id">O ID do cartão a ser deletado</param>
        /// <response code="204">Cartão excluído com sucesso</response>
        /// <response code="404">Caso o request estiver incorreto</response>
        /// <response code="500">Erro inesperado</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _cardService.DeleteCardAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir o cartão: {ex.Message}");
            }
        }
    }
}
