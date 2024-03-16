using AutoMapper;
using Domain.Interfaces;
using Domain.Models.DTOs;
using Domain.Models.Entities;

namespace Application.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public PolicyService(IPolicyRepository policyRepository, ICardRepository cardRepository, IMapper mapper)
        {
            _policyRepository = policyRepository;
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetPolicyDto>> GetAllPoliciesAsync()
        {
            List<Policy> policies = await _policyRepository.GetAllAsync();
            return policies.Select(policy => _mapper.Map<GetPolicyDto>(policy));
        }

        public async Task<GetPolicyDto> GetPolicyByIdAsync(int id)
        {
            var policy = await _policyRepository.GetByIdAsync(id);
            if (policy == null)
            {
                throw new Exception("Apólice não encontrada");
            }

            return _mapper.Map<GetPolicyDto>(policy);
        }

        public async Task<GetPolicyDto> CreatePolicyAsync(CreatePolicyDto createPolicyDto)
        {
            Card? existingCard = await _cardRepository.GetByIdAsync(createPolicyDto.CreditCardId);

            if (existingCard == null)
                throw new ArgumentException("O cartão correspondente não pode ser encontrado.");

            Policy newPolicy = _mapper.Map<Policy>(createPolicyDto);
            await _policyRepository.CreateAsync(newPolicy);

            return _mapper.Map<GetPolicyDto>(newPolicy);
            
        }

        public async Task<GetPolicyDto> UpdatePolicyAsync(int id, Policy policy)
        {
            var existingPolicy = await _policyRepository.GetByIdAsync(id);
            
            if (existingPolicy == null)
                throw new ArgumentException("Apólice não encontrada");

            existingPolicy.Number = policy.Number;
            existingPolicy.Value = policy.Value;
            existingPolicy.TriggeringDescription = policy.TriggeringDescription;

            await _policyRepository.UpdateAsync(existingPolicy);
            return _mapper.Map<GetPolicyDto>(existingPolicy);
        }

        public async Task DeletePolicyAsync(int id)
        {
            var existingPolicy = await _policyRepository.GetByIdAsync(id);
            
            if (existingPolicy == null)
            {
                throw new ArgumentException("Apólice não encontrada");
            }

            await _policyRepository.DeleteAsync(existingPolicy);
        }
    }
}
