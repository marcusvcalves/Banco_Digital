using Domain.Models.Entities;

namespace Domain.Interfaces;

public interface IAccountRepository
{
    Task<List<Account>> GetAllAsync();
    Task<Account?> GetByIdAsync(int id);
    Task<Account> CreateAsync(Account newAccount);
    Task TransferAsync(Account senderAccount, Account receiverAccount, decimal amount);
    Task UpdateAsync(Account account);
    Task DeleteAsync(Account account);
}