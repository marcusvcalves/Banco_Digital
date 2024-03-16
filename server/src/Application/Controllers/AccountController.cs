using Domain.Models.DTOs;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/v1/contas")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Retorna todas as contas.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<GetAccountDto> accountsDto = await _accountService.GetAllAccountsAsync();
            return Ok(accountsDto);
        }

        /// <summary>
        /// Retorna a conta com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da conta a ser recuperada.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetAccountDto>> GetById([FromRoute] int id)
        {
            GetAccountDto accountDto = await _accountService.GetAccountByIdAsync(id);

            if (accountDto == null)
            {
                return NotFound();
            }

            return Ok(accountDto);
        }

        /// <summary>
        /// Cria uma nova conta.
        /// </summary>
        /// <param name="createAccountDto">Os dados da nova conta a ser criada.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountDto createAccountDto)
        {
            try
            {
                var createdAccountDto = await _accountService.CreateAccountAsync(createAccountDto);
                return CreatedAtAction(nameof(GetById), new { id = createdAccountDto.Id }, createdAccountDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar a conta: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza a conta com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da conta a ser atualizado.</param>
        /// <param name="account">Os novos dados da conta.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Account account)
        {
            try
            {
                var updatedAccountDto = await _accountService.UpdateAccountAsync(id, account);
                return Ok(updatedAccountDto);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar a conta: {ex.Message}");
            }
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
                var accounts = await _accountService.TransferAsync(senderAccountId, receiverAccountId, amount);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao transferir saldo: {ex.Message}");
            }
        }

        /// <summary>
        /// Deleta a conta com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da conta a ser deletada.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _accountService.DeleteAccountAsync(id);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar a conta: {ex.Message}");
            }
        }
    }
}
