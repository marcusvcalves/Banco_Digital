using AutoMapper;
using Domain.Interfaces;
using Domain.Models.DTO.ClienteDTO;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route(template: "api/v1/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        
        public ClienteController(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Retorna todos os clientes.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Cliente> clientes = await _clienteRepository.GetAllAsync();
            var getClientesDto = clientes.Select(cliente => _mapper.Map<GetClienteDto>(cliente));
            
            return Ok(getClientesDto);
        }
        
        /// <summary>
        /// Retorna o cliente com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cliente a ser recuperado.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetClienteDto>> GetById([FromRoute] int id)
        {
            Cliente? cliente = await _clienteRepository.GetByIdAsync(id);
            
            if (cliente == null)
            {
                return NotFound();
            }
            
            GetClienteDto getClienteDto = _mapper.Map<GetClienteDto>(cliente);

            return Ok(getClienteDto);
        }
        
        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="cliente">Os dados do novo cliente a ser criado.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            Cliente novoCliente = await _clienteRepository.CreateAsync(cliente); 

            GetClienteDto getClienteDto = _mapper.Map<GetClienteDto>(novoCliente);
            
            return CreatedAtAction(nameof(GetById), new { id = novoCliente.Id }, getClienteDto);
        }
        
        /// <summary>
        /// Atualiza o cliente com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cliente a ser atualizado.</param>
        /// <param name="cliente">Os novos dados do cliente.</param>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Cliente cliente)
        {
            Cliente? clienteExistente = await _clienteRepository.GetByIdAsync(id);
            
            if (clienteExistente == null)
            {
                return NotFound();
            }
            
            await _clienteRepository.UpdateAsync(id, cliente);

            GetClienteDto getClienteDto = _mapper.Map<GetClienteDto>(clienteExistente);
            
            return Ok(getClienteDto);
        }
        
        /// <summary>
        /// Deleta o cliente com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cliente a ser deletado.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Cliente? clienteParaDeletar = await _clienteRepository.GetByIdAsync(id);

            if (clienteParaDeletar != null)
            {
                await _clienteRepository.DeleteAsync(id);

                GetClienteDto getClienteDto = _mapper.Map<GetClienteDto>(clienteParaDeletar);
                
                return Ok(getClienteDto);
            }

            return NotFound();
        }
    }
}
