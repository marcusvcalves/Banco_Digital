using Domain.Models.DTOs;
using Domain.Models.Entities;

namespace Application.Services.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<GetAccountDto>> GetAllAccountsAsync();
    Task<GetAccountDto> GetAccountByIdAsync(int id);
    Task<GetAccountDto> CreateAccountAsync(CreateAccountDto createAccountDto);
    Task<GetAccountDto> UpdateAccountAsync(int id, Account account);
    Task<bool> DeleteAccountAsync(int id);
    Task<List<GetAccountDto>> TransferAsync(int senderAccountId, int receiverAccountId, decimal amount);
}


