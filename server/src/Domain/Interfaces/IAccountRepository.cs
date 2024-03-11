using Domain.Models.Entities;

namespace Domain.Interfaces;

public interface IAccountRepository
{
    Task<List<Account>> GetAllAsync();
    Task<Account?> GetByIdAsync(int id);
    Task<Account> CreateAsync(Account newAccount);
    Task TransferAsync(int senderAccountId, int receiverAccountId, decimal amount);
    Task UpdateAsync(int id, Account account);
    Task DeleteAsync(int id);
}