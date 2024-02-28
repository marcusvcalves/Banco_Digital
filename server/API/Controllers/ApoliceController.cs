using AutoMapper;
using Domain.Models.DTO.ApoliceSeguroDTO;
using Domain.Models.Entities;
using Infra.Repositories.ApoliceRepo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route(template: "api/v1/apolices")]
public class ApoliceController : ControllerBase
{
    private readonly IApoliceRepository _repository;
    private readonly IMapper _mapper;

    public ApoliceController(IApoliceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Apolice> apolices = await _repository.GetAllAsync();
        
        var getApolicesDto = apolices.Select(apolice => _mapper.Map<GetApoliceDto>(apolice));

        return Ok(getApolicesDto);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GetApoliceDto>> GetById(int id)
    {
        Apolice apolice = await _repository.GetByIdAsync(id);

        if (apolice == null)
        {
            return NotFound();
        }

        GetApoliceDto getApoliceDto = _mapper.Map<GetApoliceDto>(apolice);

        return Ok(getApoliceDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Apolice apolice)
    {
        Apolice novaApolice = await _repository.CreateAsync(apolice);

        return CreatedAtAction(nameof(GetById), new { id = apolice.Id }, novaApolice);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update (int id, Apolice apolice)
    {
        var apoliceExistente = await _repository.GetByIdAsync(id);
        if (apoliceExistente == null)
        {
            return NotFound();
        }
        
        await _repository.UpdateAsync(id, apolice);
        
        GetApoliceDto getApoliceDto = _mapper.Map<GetApoliceDto>(apoliceExistente);

        return Ok(getApoliceDto);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Apolice apoliceParaDeletar = await _repository.GetByIdAsync(id);

        if (apoliceParaDeletar != null)
        {
            await _repository.DeleteAsync(id);
            
            GetApoliceDto getApoliceDto = _mapper.Map<GetApoliceDto>(apoliceParaDeletar);
            
            return Ok(getApoliceDto);
        }

        return NotFound();
    }
}