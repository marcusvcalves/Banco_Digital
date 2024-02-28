using AutoMapper;
using Domain.Models.DTO.CartaoDTO;
using Domain.Models.Entities;
using Infra.Repositories.CartaoRepo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route(template: "api/v1/cartoes")]
public class CartaoController : ControllerBase
{
    private readonly ICartaoRepository _repository;
    private readonly IMapper _mapper;
    
    public CartaoController(ICartaoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Cartao> cartoes = await _repository.GetAllAsync();
        var getCartoesDto = cartoes.Select(cartao => _mapper.Map<GetCartaoDto>(cartao));

        return Ok(getCartoesDto);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GetCartaoDto>> GetById(int id)
    {
        Cartao cartao = await _repository.GetByIdAsync(id);

        if (cartao == null)
        {
            return NotFound();
        }

        GetCartaoDto getCartaoDto = _mapper.Map<GetCartaoDto>(cartao);

        return Ok(getCartaoDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Cartao cartao)
    {
        Cartao novoCartao = await _repository.CreateAsync(cartao); 

        return CreatedAtAction(nameof(GetById), new { id = novoCartao.Id }, novoCartao);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Cartao cartao)
    {
        var cartaoExistente = await _repository.GetByIdAsync(id);
        if (cartaoExistente == null)
        {
            return NotFound();
        }
        
        await _repository.UpdateAsync(id, cartao);
        
        GetCartaoDto getCartaoDto = _mapper.Map<GetCartaoDto>(cartaoExistente);

        return Ok(getCartaoDto);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Cartao cartaoParaDeletar = await _repository.GetByIdAsync(id);

        if (cartaoParaDeletar != null)
        {
            await _repository.DeleteAsync(id);

            GetCartaoDto getCartaoDto = _mapper.Map<GetCartaoDto>(cartaoParaDeletar);
            
            return Ok(getCartaoDto);
        }

        return NotFound();
    }
}