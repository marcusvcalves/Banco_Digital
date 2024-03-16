using AutoMapper;
using Domain.Interfaces;
using Domain.Models.DTOs;
using Domain.Models.Entities;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IClientRepository clientRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAccountDto>> GetAllAccountsAsync()
        {
            List<Account> accounts = await _accountRepository.GetAllAsync();
            return accounts.Select(account => _mapper.Map<GetAccountDto>(account));
        }

        public async Task<GetAccountDto> GetAccountByIdAsync(int id)
        {
            Account? account = await _accountRepository.GetByIdAsync(id);
            return _mapper.Map<GetAccountDto>(account);
        }

        public async Task<GetAccountDto> CreateAccountAsync(CreateAccountDto createAccountDto)
        {
            try
            {
                Client? existingClient = await _clientRepository.GetByIdAsync(createAccountDto.ClientId);

                if (existingClient == null)
                    throw new ArgumentException("O cliente correspondente não pode ser encontrado.");

                Account newAccount = _mapper.Map<Account>(createAccountDto);
                await _accountRepository.CreateAsync(newAccount);

                return _mapper.Map<GetAccountDto>(newAccount);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar conta: {ex.Message}.");
            }
        }

        public async Task<GetAccountDto> UpdateAccountAsync(int id, Account account)
        {
            Account? existingAccount = await _accountRepository.GetByIdAsync(id);

            if (existingAccount == null)
                throw new ArgumentException("A conta especificada não existe.");

            existingAccount.Balance = account.Balance;
            existingAccount.Password = account.Password;

            await _accountRepository.UpdateAsync(existingAccount);

            return _mapper.Map<GetAccountDto>(existingAccount);
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            Account? accountToDelete = await _accountRepository.GetByIdAsync(id);

            if (accountToDelete == null)
                throw new ArgumentException("A conta especificada não existe.");

            await _accountRepository.DeleteAsync(accountToDelete);
            return true;
        }

        public async Task<List<GetAccountDto>> TransferAsync(int senderAccountId, int receiverAccountId, decimal amount)
        {
            GetAccountDto senderAccountDto = await GetAccountByIdAsync(senderAccountId);
            GetAccountDto receiverAccountDto = await GetAccountByIdAsync(receiverAccountId);
            
            if (senderAccountDto == null || receiverAccountDto == null)
            {
                throw new Exception("Uma ou ambas as contas não foram encontradas.");
            }

            if (senderAccountDto.Balance < amount)
            {
                throw new Exception("Saldo insuficiente na conta de origem.");
            }
            
            List<Account> accounts = await _accountRepository.TransferAsync(senderAccountId, receiverAccountId, amount);
            
            List<GetAccountDto> accountDtos = new List<GetAccountDto>();
            
            foreach (Account account in accounts)
            {
                GetAccountDto accountDto = _mapper.Map<GetAccountDto>(account);
                accountDtos.Add(accountDto);
            }

            return accountDtos;
        }
    }
}
