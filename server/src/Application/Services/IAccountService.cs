using Domain.Models.DTOs;
using Domain.Models.Entities;

namespace Application.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<GetAccountDto>> GetAllAccountsAsync();
        Task<GetAccountDto> GetAccountByIdAsync(int id);
        Task<GetAccountDto> CreateAccountAsync(CreateAccountDto createAccountDto);
        Task<GetAccountDto> UpdateAccountAsync(int id, Account account);
        Task<bool> DeleteAccountAsync(int id);
        Task TransferAsync(int senderAccountId, int receiverAccountId, decimal amount);
    }
}

