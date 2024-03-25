using Domain.Models.DTOs;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/v1/clientes")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        
        /// <summary>
        /// Retorna todos os clientes
        /// </summary>
        /// <response code="200">Retorna lista com os DTOs de todos os clientes</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<GetClientDto> getClientsDto = await _clientService.GetAllClientsAsync();
            return Ok(getClientsDto);
        }
        
        /// <summary>
        /// Retorna o cliente com o ID especificado
        /// </summary>
        /// <param name="id">O ID do cliente a ser recuperado</param>
        /// <response code="200">Retorna o DTO do cliente especificado</response>
        /// <response code="404">Caso não for encontrado cliente com o ID especificado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetClientDto>> GetById([FromRoute] int id)
        {
            GetClientDto getClientDto = await _clientService.GetClientByIdAsync(id);

            if (getClientDto == null) 
                return NotFound();

            return Ok(getClientDto);
        }
        
        /// <summary>
        /// Cria um novo cliente
        /// </summary>
        /// <param name="createClientDto">Os dados do novo cliente a ser criado</param>
        /// <response code="201">Cria o novo cliente e retorna seu DTO</response>
        /// <response code="500">Erro inesperado</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientDto createClientDto)
        {
            try
            {
                GetClientDto newClientDto = await _clientService.CreateClientAsync(createClientDto); 
                return CreatedAtAction(nameof(GetById), new { id = newClientDto.Id }, newClientDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar o cliente: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Atualiza o cliente com o ID especificado
        /// </summary>
        /// <param name="id">O ID do cliente a ser atualizado</param>
        /// <param name="client">Os novos dados do cliente</param>
        /// <response code="200">Retorna o DTO atualizado do cliente</response>
        /// <response code="404">Caso não exista cliente com o ID especificado</response>
        /// <response code="500">Erro inesperado</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Client client)
        {
            try
            {
                var updatedClientDto = await _clientService.UpdateClientAsync(id, client);
                return Ok(updatedClientDto);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar o cliente: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Deleta o cliente com o ID especificado
        /// </summary>
        /// <param name="id">O ID do cliente a ser deletado</param>
        /// <response code="204">Cliente excluído com sucesso</response>
        /// <response code="404">Caso o ID for incorreto</response>
        /// <response code="500">Erro inesperado</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _clientService.DeleteClientAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir o cliente: {ex.Message}");
            }
        }
    }
}
