using Domain.Interfaces;
using Domain.Models.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;
    
    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Account>> GetAllAsync()
    {
        return await _context.Accounts.ToListAsync();
    }

    public async Task<Account?> GetByIdAsync(int id)
    {
        return await _context.Accounts.FindAsync(id);
    }

    public async Task<Account> CreateAsync(Account newAccount)
    {
        _context.Accounts.Add(newAccount);
        await _context.SaveChangesAsync();

        return newAccount;
    }
    
    public async Task TransferAsync(int senderAccountId, int receiverAccountId, decimal amount)
    {
        Account? senderAccount = await GetByIdAsync(senderAccountId);
        Account? receiverAccount = await GetByIdAsync(receiverAccountId);

        if (senderAccount == null || receiverAccount == null)
        {
            throw new Exception("Uma ou ambas as contas não foram encontradas.");
        }

        if (senderAccount.Balance < amount)
        {
            throw new Exception("Saldo insuficiente na conta de origem.");
        }
        
        senderAccount.Balance -= amount;
        receiverAccount.Balance += amount;

        _context.Entry(senderAccount).State = EntityState.Modified;
        _context.Entry(receiverAccount).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, Account account)
    {
        Account? existingAccount = await GetByIdAsync(id);

        if (existingAccount != null)
        {
            existingAccount.Balance = account.Balance;
            existingAccount.Password = account.Password;
            
            _context.Entry(existingAccount).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        Account? account = await GetByIdAsync(id);

        if (account != null)
        {
            _context.Accounts.Remove(account);

            await _context.SaveChangesAsync();
        }
    }
}