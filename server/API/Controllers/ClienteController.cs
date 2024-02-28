using AutoMapper;
using Domain.Models.DTO.ClienteDTO;
using Domain.Models.Entities;
using Infra.Repositories.ClienteRepo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Cliente> clientes = await _repository.GetAllAsync();
        var getClientesDto = clientes.Select(cliente => _mapper.Map<GetClienteDto>(cliente));
        
        return Ok(getClientesDto);
    }
    
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
    
    [HttpPost]
    public async Task<IActionResult> Create(Cliente cliente)
    {
        Cliente novoCliente = await _repository.CreateAsync(cliente); 

        return CreatedAtAction(nameof(GetById), new { id = novoCliente.Id }, novoCliente);
    }
    
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