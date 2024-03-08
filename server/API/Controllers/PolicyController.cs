using AutoMapper;
using Domain.Interfaces;
using Domain.Models.DTOs;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route(template: "api/v1/apolices")]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IMapper _mapper;

        public PolicyController(IPolicyRepository policyRepository, IMapper mapper)
        {
            _policyRepository = policyRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todas as apólices de seguro.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Policy> policies = await _policyRepository.GetAllAsync();
        
            var getPoliciesDto = policies.Select(policy => _mapper.Map<GetPolicyDto>(policy));

            return Ok(getPoliciesDto);
        }
        
        /// <summary>
        /// Retorna a apólice de seguro com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da apólice de seguro a ser recuperada.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPolicyDto>> GetById([FromRoute] int id)
        {
            Policy? policy = await _policyRepository.GetByIdAsync(id);

            if (policy == null)
            {
                return NotFound();
            }

            GetPolicyDto getPolicyDto = _mapper.Map<GetPolicyDto>(policy);

            return Ok(getPolicyDto);
        }
        
        /// <summary>
        /// Cria uma nova apólice de seguro.
        /// </summary>
        /// <param name="policy">Os dados da nova apólice de seguro a ser criada.</param>
        [HttpPost]
        public async Task<IActionResult> Create(Policy policy)
        {
            Policy newPolicy = await _policyRepository.CreateAsync(policy);
            
            GetPolicyDto getPolicyDto = _mapper.Map<GetPolicyDto>(newPolicy);

            return CreatedAtAction(nameof(GetById), new { id = policy.Id }, getPolicyDto);
        }
        
        /// <summary>
        /// Atualiza a apólice de seguro com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da apólice de seguro a ser atualizada.</param>
        /// <param name="policy">Os novos dados da apólice de seguro.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update ([FromRoute] int id, [FromBody] Policy policy)
        {
            Policy? existingPolicy = await _policyRepository.GetByIdAsync(id);
            
            if (existingPolicy == null)
            {
                return NotFound();
            }
        
            await _policyRepository.UpdateAsync(id, policy);
        
            GetPolicyDto getPolicyDto = _mapper.Map<GetPolicyDto>(existingPolicy);

            return Ok(getPolicyDto);
        }
        
        /// <summary>
        /// Deleta a apólice de seguro com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da apólice de seguro a ser deletada.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Policy? policyToDelete = await _policyRepository.GetByIdAsync(id);

            if (policyToDelete != null)
            {
                await _policyRepository.DeleteAsync(id);
            
                return NoContent();
            }

            return NotFound();
        }
    }
}
