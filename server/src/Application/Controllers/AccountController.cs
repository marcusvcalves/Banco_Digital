using Domain.Models.DTOs;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Services.Interfaces;
using Domain.Exceptions;

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
        /// Retorna todas as contas
        /// </summary>
        /// <response code="200">Lista com os DTOs de todas as contas</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<GetAccountDto> accountsDto = await _accountService.GetAllAccountsAsync();
            return Ok(accountsDto);
        }

        /// <summary>
        /// Retorna a conta com o ID especificado
        /// </summary>
        /// <param name="id">O ID da conta a ser recuperada</param>
        /// <response code="200">Retorna o DTO da conta</response>
        /// <response code="404">Caso não encontre conta com o ID</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetAccountDto>> GetById([FromRoute] int id)
        {
            GetAccountDto accountDto = await _accountService.GetAccountByIdAsync(id);

            if (accountDto == null)
                return NotFound();

            return Ok(accountDto);
        }

        /// <summary>
        /// Cria uma nova conta
        /// </summary>
        /// <param name="createAccountDto">Os dados da nova conta a ser criada</param>
        /// <response code="201">Retorna o DTO da conta criada</response>
        /// <response code="400">Caso o request estiver incorreto</response>
        /// <response code="500">Erro inesperado</response>
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
        /// Atualiza a conta com o ID especificado
        /// </summary>
        /// <param name="id">O ID da conta a ser atualizado</param>
        /// <param name="account">Os novos dados da conta</param>
        /// <response code="200">Retorna o DTO da conta atualizada</response>
        /// <response code="400">Caso o request estiver incorreto</response>
        /// <response code="500">Erro inesperado</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Account account)
        {
            try
            {
                var updatedAccountDto = await _accountService.UpdateAccountAsync(id, account);
                return Ok(updatedAccountDto);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar a conta: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Transfere saldo de uma conta para outra
        /// </summary>
        /// <param name="senderAccountId">O ID da conta de origem</param>
        /// <param name="receiverAccountId">O ID da conta de destino</param>
        /// <param name="amount">A quantia a ser transferida</param>
        /// <response code="200">Caso a transferência seja realizada, retorna uma lista com a conta enviante e a conta recebedora</response>
        /// <response code="400">Em casos de ID's incorretos ou saldo insuficiente</response>
        /// <response code="500">Erro inesperado</response>
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
                if (ex is AccountNotFoundException || ex is InsufficientBalanceException)
                {
                    return BadRequest(ex.Message);
                }
                
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao transferir saldo: {ex.Message}");
            }
        }

        /// <summary>
        /// Deleta a conta com o ID especificado
        /// </summary>
        /// <param name="id">O ID da conta a ser deletada</param>
        /// <response code="204">Conta excluída com sucesso</response>
        /// <response code="404">Caso ID for incorreto</response>
        /// <response code="500">Erro inesperado</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _accountService.DeleteAccountAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir a conta: {ex.Message}");
            }
        }
    }
}
