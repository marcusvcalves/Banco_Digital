using AutoMapper;
using Domain.Interfaces;
using Domain.Models.DTO.CartaoDTO;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route(template: "api/v1/cartoes")]
    public class CartaoController : ControllerBase
    {
        private readonly ICartaoRepository _cartaoRepository;
        private readonly IMapper _mapper;
        
        public CartaoController(ICartaoRepository cartaoRepository, IMapper mapper)
        {
            _cartaoRepository = cartaoRepository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Retorna todos os cartões.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Cartao> cartoes = await _cartaoRepository.GetAllAsync();
            var getCartoesDto = cartoes.Select(cartao => _mapper.Map<GetCartaoDto>(cartao));

            return Ok(getCartoesDto);
        }
        
        /// <summary>
        /// Retorna o cartão com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cartão a ser recuperado.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCartaoDto>> GetById([FromRoute] int id)
        {
            Cartao? cartao = await _cartaoRepository.GetByIdAsync(id);

            if (cartao == null)
            {
                return NotFound();
            }

            GetCartaoDto getCartaoDto = _mapper.Map<GetCartaoDto>(cartao);

            return Ok(getCartaoDto);
        }
        
        /// <summary>
        /// Cria um novo cartão.
        /// </summary>
        /// <param name="cartao">Os dados do novo cartão a ser criado.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cartao cartao)
        {
            Cartao novoCartao = await _cartaoRepository.CreateAsync(cartao); 

            return CreatedAtAction(nameof(GetById), new { id = novoCartao.Id }, novoCartao);
        }
        
        /// <summary>
        /// Atualiza o cartão com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cartão a ser atualizado.</param>
        /// <param name="cartao">Os novos dados do cartão.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Cartao cartao)
        {
            Cartao? cartaoExistente = await _cartaoRepository.GetByIdAsync(id);
            
            if (cartaoExistente == null)
            {
                return NotFound();
            }
            
            await _cartaoRepository.UpdateAsync(id, cartao);
            
            GetCartaoDto getCartaoDto = _mapper.Map<GetCartaoDto>(cartaoExistente);

            return Ok(getCartaoDto);
        }
        
        /// <summary>
        /// Deleta o cartão com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cartão a ser deletado.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Cartao? cartaoParaDeletar = await _cartaoRepository.GetByIdAsync(id);

            if (cartaoParaDeletar != null)
            {
                await _cartaoRepository.DeleteAsync(id);

                GetCartaoDto getCartaoDto = _mapper.Map<GetCartaoDto>(cartaoParaDeletar);
                
                return Ok(getCartaoDto);
            }

            return NotFound();
        }
    }
}
