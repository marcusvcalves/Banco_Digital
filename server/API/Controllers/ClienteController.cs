using AutoMapper;
using Domain.Models.DTO.ClienteDTO;
using Domain.Models.Entities;
using Infra.Repositories.ClienteRepo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route(template: "api/v1/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;
        
        public ClienteController(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Retorna todos os clientes.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Cliente> clientes = await _repository.GetAllAsync();
            var getClientesDto = clientes.Select(cliente => _mapper.Map<GetClienteDto>(cliente));
            
            return Ok(getClientesDto);
        }
        
        /// <summary>
        /// Retorna o cliente com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cliente a ser recuperado.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetClienteDto>> GetById(int id)
        {
            Cliente cliente = await _repository.GetByIdAsync(id);
            
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
        public async Task<IActionResult> Create(Cliente cliente)
        {
            Cliente novoCliente = await _repository.CreateAsync(cliente); 

            return CreatedAtAction(nameof(GetById), new { id = novoCliente.Id }, novoCliente);
        }
        
        /// <summary>
        /// Atualiza o cliente com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cliente a ser atualizado.</param>
        /// <param name="cliente">Os novos dados do cliente.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Cliente cliente)
        {
            Cliente clienteExistente = await _repository.GetByIdAsync(id);
            if (clienteExistente == null)
            {
                return NotFound();
            }
            
            await _repository.UpdateAsync(id, cliente);

            GetClienteDto getClienteDto = _mapper.Map<GetClienteDto>(clienteExistente);
            
            return Ok(clienteExistente);
        }
        
        /// <summary>
        /// Deleta o cliente com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cliente a ser deletado.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Cliente clienteParaDeletar = await _repository.GetByIdAsync(id);

            if (clienteParaDeletar != null)
            {
                await _repository.DeleteAsync(id);

                GetClienteDto getClienteDto = _mapper.Map<GetClienteDto>(clienteParaDeletar);
                
                return Ok(getClienteDto);
            }

            return NotFound();
        }
    }
}
