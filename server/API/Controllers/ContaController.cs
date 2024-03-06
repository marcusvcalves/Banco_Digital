using AutoMapper;
using Domain.Interfaces;
using Domain.Models.DTO.ContaDTO;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route(template: "api/v1/contas")]
    public class ContaController : ControllerBase
    {
        private readonly IContaRepository _contaRepository;
        private readonly IMapper _mapper;

        public ContaController(IContaRepository contaRepository, IMapper mapper)
        {
            _contaRepository = contaRepository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Retorna todas as contas.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Conta> contas = await _contaRepository.GetAllAsync();
            var getContasDto = contas.Select(conta => _mapper.Map<GetContaDto>(conta));
            
            return Ok(getContasDto);
        }
        
        /// <summary>
        /// Retorna a conta com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da conta a ser recuperada.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetContaDto>> GetById([FromRoute] int id)
        {
            Conta? conta = await _contaRepository.GetByIdAsync(id);
            
            if (conta == null)
            {
                return NotFound();
            }
            
            GetContaDto getContaDto = _mapper.Map<GetContaDto>(conta);

            return Ok(getContaDto);
        }
        
        /// <summary>
        /// Cria uma nova conta.
        /// </summary>
        /// <param name="conta">Os dados da nova conta a ser criada.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Conta conta)
        {
            Conta novaConta = await _contaRepository.CreateAsync(conta); 

            return CreatedAtAction(nameof(GetById), new { id = novaConta.Id }, novaConta);
        }
        
        /// <summary>
        /// Transfere saldo de uma conta para outra.
        /// </summary>
        /// <param name="idContaEnvio">O ID da conta de origem.</param>
        /// <param name="idContaRecebedora">O ID da conta de destino.</param>
        /// <param name="quantia">A quantia a ser transferida.</param>
        [HttpPost("{idContaEnvio}/{idContaRecebedora}/{quantia}")]
        public async Task TransferAsync([FromRoute] int idContaEnvio, [FromRoute] int idContaRecebedora, [FromRoute] decimal quantia)
        {
            await _contaRepository.TransferAsync(idContaEnvio, idContaRecebedora, quantia);
        }
        
        /// <summary>
        /// Atualiza a conta com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da conta a ser atualizada.</param>
        /// <param name="conta">Os novos dados da conta.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Conta conta)
        {
            Conta? contaExistente = await _contaRepository.GetByIdAsync(id);
            
            if (contaExistente == null)
            {
                return NotFound();
            }
            
            await _contaRepository.UpdateAsync(id, conta);
            
            GetContaDto getContaDto = _mapper.Map<GetContaDto>(contaExistente);

            return Ok(getContaDto);
        }
        
        /// <summary>
        /// Deleta a conta com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da conta a ser deletada.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Conta? contaParaDeletar = await _contaRepository.GetByIdAsync(id);

            if (contaParaDeletar != null)
            {
                await _contaRepository.DeleteAsync(id);

                GetContaDto getCartaoDto = _mapper.Map<GetContaDto>(contaParaDeletar);
                
                return Ok(contaParaDeletar);
            }

            return NotFound();
        }
    }
}
