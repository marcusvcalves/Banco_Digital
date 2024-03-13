using Domain.Models.DTOs;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/v1/apolices")]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        /// <summary>
        /// Retorna todas as apólices de seguro.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<GetPolicyDto> getPoliciesDto = await _policyService.GetAllPoliciesAsync();
            return Ok(getPoliciesDto);
        }
        
        /// <summary>
        /// Retorna a apólice de seguro com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da apólice de seguro a ser recuperada.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPolicyDto>> GetById([FromRoute] int id)
        {
            GetPolicyDto getPolicyDto = await _policyService.GetPolicyByIdAsync(id);
            if (getPolicyDto == null)
            {
                return NotFound();
            }
            return Ok(getPolicyDto);
        }
        
        /// <summary>
        /// Cria uma nova apólice de seguro.
        /// </summary>
        /// <param name="createPolicyDto">Os dados da nova apólice de seguro a ser criada.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePolicyDto createPolicyDto)
        {
            GetPolicyDto newPolicy = await _policyService.CreatePolicyAsync(createPolicyDto);
            return CreatedAtAction(nameof(GetById), new { id = newPolicy.Id }, newPolicy);
        }
        
        /// <summary>
        /// Atualiza a apólice de seguro com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da apólice de seguro a ser atualizada.</param>
        /// <param name="policy">Os novos dados da apólice de seguro.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update ([FromRoute] int id, [FromBody] Policy policy)
        {
            try
            {
                var updatedPolicyDto = await _policyService.UpdatePolicyAsync(id, policy);
                return Ok(updatedPolicyDto);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        
        /// <summary>
        /// Deleta a apólice de seguro com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da apólice de seguro a ser deletada.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (await _policyService.DeletePolicyAsync(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
