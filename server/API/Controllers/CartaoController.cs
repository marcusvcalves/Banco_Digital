using AutoMapper;
using Domain.Models.DTO.CartaoDTO;
using Domain.Models.Entities;
using Infra.Repositories.CartaoRepo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
        
        /// <summary>
        /// Retorna todos os cartões.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Cartao> cartoes = await _repository.GetAllAsync();
            var getCartoesDto = cartoes.Select(cartao => _mapper.Map<GetCartaoDto>(cartao));

            return Ok(getCartoesDto);
        }
        
        /// <summary>
        /// Retorna o cartão com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cartão a ser recuperado.</param>
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
        
        /// <summary>
        /// Cria um novo cartão.
        /// </summary>
        /// <param name="cartao">Os dados do novo cartão a ser criado.</param>
        [HttpPost]
        public async Task<IActionResult> Create(Cartao cartao)
        {
            Cartao novoCartao = await _repository.CreateAsync(cartao); 

            return CreatedAtAction(nameof(GetById), new { id = novoCartao.Id }, novoCartao);
        }
        
        /// <summary>
        /// Atualiza o cartão com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cartão a ser atualizado.</param>
        /// <param name="cartao">Os novos dados do cartão.</param>
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
        
        /// <summary>
        /// Deleta o cartão com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do cartão a ser deletado.</param>
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
}
