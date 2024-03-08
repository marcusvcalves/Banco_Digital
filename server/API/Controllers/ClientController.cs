using AutoMapper;
using Domain.Interfaces;
using Domain.Models.DTOs;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route(template: "api/v1/clientes")]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        
        public ClientController(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Retorna todos os clientes.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Client> clients = await _clientRepository.GetAllAsync();
            var getClientsDto = clients.Select(client => _mapper.Map<GetClientDto>(client));
            
            return Ok(getClientsDto);
        }
        
        /// <summary>
        /// Retorna o cliente com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cliente a ser recuperado.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetClientDto>> GetById([FromRoute] int id)
        {
            Client? client = await _clientRepository.GetByIdAsync(id);
            
            if (client == null)
            {
                return NotFound();
            }
            
            GetClientDto getClientDto = _mapper.Map<GetClientDto>(client);

            return Ok(getClientDto);
        }
        
        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="client">Os dados do novo cliente a ser criado.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Client client)
        {
            Client newClient = await _clientRepository.CreateAsync(client); 

            GetClientDto getClientDto = _mapper.Map<GetClientDto>(newClient);
            
            return CreatedAtAction(nameof(GetById), new { id = newClient.Id }, getClientDto);
        }
        
        /// <summary>
        /// Atualiza o cliente com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cliente a ser atualizado.</param>
        /// <param name="client">Os novos dados do cliente.</param>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Client client)
        {
            Client? existingClient = await _clientRepository.GetByIdAsync(id);
            
            if (existingClient == null)
            {
                return NotFound();
            }
            
            await _clientRepository.UpdateAsync(id, client);

            GetClientDto getClientDto = _mapper.Map<GetClientDto>(existingClient);
            
            return Ok(getClientDto);
        }
        
        /// <summary>
        /// Deleta o cliente com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cliente a ser deletado.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Client? clientToDelete = await _clientRepository.GetByIdAsync(id);

            if (clientToDelete != null)
            {
                await _clientRepository.DeleteAsync(id);

                return NoContent();
            }

            return NotFound();
        }
    }
}
