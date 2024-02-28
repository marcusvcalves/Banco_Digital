using AutoMapper;
using Domain.Models.DTO.ContaDTO;
using Domain.Models.Entities;
using Infra.Repositories.ContaRepo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route(template: "api/v1/contas")]
public class ContaController : ControllerBase
{
    private readonly IContaRepository _repository;
    private readonly IMapper _mapper;

    public ContaController(IContaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Conta> contas = await _repository.GetAllAsync();
        var getContasDto = contas.Select(conta => _mapper.Map<GetContaDto>(conta));
        
        return Ok(getContasDto);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GetContaDto>> GetById(int id)
    {
        Conta conta = await _repository.GetByIdAsync(id);
        
        if (conta == null)
        {
            return NotFound();
        }
        
        GetContaDto getContaDto = _mapper.Map<GetContaDto>(conta);

        return Ok(getContaDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Conta conta)
    {
        Conta novaConta = await _repository.CreateAsync(conta); 

        return CreatedAtAction(nameof(GetById), new { id = novaConta.Id }, novaConta);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Conta conta)
    {
        var contaExistente = await _repository.GetByIdAsync(id);
        if (contaExistente == null)
        {
            return NotFound();
        }
        
        await _repository.UpdateAsync(id, conta);
        
        GetContaDto getContaDto = _mapper.Map<GetContaDto>(contaExistente);

        return Ok(getContaDto);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Conta contaParaDeletar = await _repository.GetByIdAsync(id);

        if (contaParaDeletar != null)
        {
            await _repository.DeleteAsync(id);

            GetContaDto getCartaoDto = _mapper.Map<GetContaDto>(contaParaDeletar);
            
            return Ok(contaParaDeletar);
        }

        return NotFound();
    }
}