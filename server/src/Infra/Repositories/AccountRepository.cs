using Domain.Interfaces;
using Domain.Models.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Account>> GetAllAsync()
        {
            return await _context.Accounts.Include(account => account.Client).ToListAsync();
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

        public async Task UpdateAsync(Account account)
        {
            _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Account account)
        {
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Account>> TransferAsync(int senderAccountId, int receiverAccountId, decimal amount)
        {
            Account? senderAccount = _context.Accounts.FirstOrDefault(acc => acc.Id == senderAccountId);
            Account? receiverAccount = _context.Accounts.FirstOrDefault(acc => acc.Id == receiverAccountId);
            
            if (senderAccount == null || receiverAccount == null)
            {
                throw new ArgumentException($"Conta enviante ou recebedora não encontrada.");
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    senderAccount.Balance -= amount;                
                    receiverAccount.Balance += amount;
                    await _context.SaveChangesAsync();
                
                    await transaction.CommitAsync();
                
                    senderAccount = await _context.Accounts.FirstOrDefaultAsync(acc => acc.Id == senderAccountId);
                    receiverAccount = await _context.Accounts.FirstOrDefaultAsync(acc => acc.Id == receiverAccountId);
                
                    if (senderAccount == null || receiverAccount == null)
                    {
                        throw new ArgumentException($"Conta enviante ou recebedora não encontrada.");
                    }

                    return new List<Account> { senderAccount, receiverAccount};

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Erro ao transferir fundos: {ex.Message}.");
                }
            };
        }
    }
}
