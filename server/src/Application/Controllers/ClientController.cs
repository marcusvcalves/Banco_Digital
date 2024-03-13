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
        /// Retorna todos os clientes.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<GetClientDto> getClientsDto = await _clientService.GetAllClientsAsync();
            
            return Ok(getClientsDto);
        }
        
        /// <summary>
        /// Retorna o cliente com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cliente a ser recuperado.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetClientDto>> GetById([FromRoute] int id)
        {
            try
            {
                var clientDto = await _clientService.GetClientByIdAsync(id);
                return Ok(clientDto);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        
        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="createClientDto">Os dados do novo cliente a ser criado.</param>
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
        /// Atualiza o cliente com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cliente a ser atualizado.</param>
        /// <param name="client">Os novos dados do cliente.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Client client)
        {
            try
            {
                var updatedClientDto = await _clientService.UpdateClientAsync(id, client);
                return Ok(updatedClientDto);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar o cliente: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Deleta o cliente com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cliente a ser deletado.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var success = await _clientService.DeleteClientAsync(id);
                if (success)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar o cliente: {ex.Message}");
            }
        }
    }
}
