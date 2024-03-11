using AutoMapper;
using Domain.Interfaces;
using Domain.Models.DTOs;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route(template: "api/v1/contas")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountController(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Retorna todas as contas.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Account> accounts = await _accountRepository.GetAllAsync();
            var getAccountsDto = accounts.Select(account => _mapper.Map<GetAccountDto>(account));
            
            return Ok(getAccountsDto);
        }
        
        /// <summary>
        /// Retorna a conta com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da conta a ser recuperada.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetAccountDto>> GetById([FromRoute] int id)
        {
            Account? account = await _accountRepository.GetByIdAsync(id);
            
            if (account == null)
            {
                return NotFound();
            }
            
            GetAccountDto getAccountDto = _mapper.Map<GetAccountDto>(account);

            return Ok(getAccountDto);
        }
        
        /// <summary>
        /// Cria uma nova conta.
        /// </summary>
        /// <param name="account">Os dados da nova conta a ser criada.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Account account)
        {
            Account newAccount = await _accountRepository.CreateAsync(account);
            
            GetAccountDto getAccountDto = _mapper.Map<GetAccountDto>(newAccount);

            return CreatedAtAction(nameof(GetById), new { id = newAccount.Id }, getAccountDto);
        }
        
        /// <summary>
        /// Transfere saldo de uma conta para outra.
        /// </summary>
        /// <param name="senderAccountId">O ID da conta de origem.</param>
        /// <param name="receiverAccountId">O ID da conta de destino.</param>
        /// <param name="amount">A quantia a ser transferida.</param>
        [HttpPost("{senderAccountId}/{receiverAccountId}/{amount}")]
        public async Task<IActionResult> TransferAsync([FromRoute] int senderAccountId, [FromRoute] int receiverAccountId, [FromRoute] decimal amount)
        {
            try
            {
                await _accountRepository.TransferAsync(senderAccountId, receiverAccountId, amount);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao processar a transferência: " + ex.Message);
            }
        }
        
        /// <summary>
        /// Atualiza a conta com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da conta a ser atualizada.</param>
        /// <param name="account">Os novos dados da conta.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Account account)
        {
            Account? existingAccount = await _accountRepository.GetByIdAsync(id);
            
            if (existingAccount == null)
            {
                return NotFound();
            }
            
            await _accountRepository.UpdateAsync(id, account);
            
            GetAccountDto getAccountDto = _mapper.Map<GetAccountDto>(existingAccount);

            return Ok(getAccountDto);
        }
        
        /// <summary>
        /// Deleta a conta com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da conta a ser deletada.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Account? accountToDelete = await _accountRepository.GetByIdAsync(id);

            if (accountToDelete != null)
            {
                await _accountRepository.DeleteAsync(id);

                return NoContent();
            }

            return NotFound();
        }
    }
}
