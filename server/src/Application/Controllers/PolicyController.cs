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
        /// <response code="200">Retorna lista com os DTOs de todas as apólices</response>
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
        /// <response code="200">Retorna DTO da apólice</response>
        /// <response code="404">Caso ID da apólice estiver incorreto</response>

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
        /// <response code="201">Cria a nova apólice e retorna seu DTO</response>
        /// <response code="400">Caso o request estiver incorreto</response>
        /// <response code="500">Erro inesperado</response>

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePolicyDto createPolicyDto)
        {
            try
            {
                GetPolicyDto newPolicyDto = await _policyService.CreatePolicyAsync(createPolicyDto);
                return CreatedAtAction(nameof(GetById), new { id = newPolicyDto.Id }, newPolicyDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar a apólice: {ex.Message}");
            }
            
        }
        
        /// <summary>
        /// Atualiza a apólice de seguro com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da apólice de seguro a ser atualizada.</param>
        /// <param name="policy">Os novos dados da apólice de seguro.</param>
        /// <response code="200">Retorna o DTO da apólice atualizada</response>
        /// <response code="404">Caso o request estiver incorreto</response>
        /// <response code="500">Erro inesperado</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update ([FromRoute] int id, [FromBody] Policy policy)
        {
            try
            {
                var updatedPolicyDto = await _policyService.UpdatePolicyAsync(id, policy);
                return Ok(updatedPolicyDto);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        /// <summary>
        /// Deleta a apólice de seguro com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da apólice de seguro a ser deletada.</param>
        /// <response code="204">Apólice excluída com sucesso</response>
        /// <response code="404">Caso o ID for incorreto</response>
        /// <response code="500">Erro inesperado</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _policyService.DeletePolicyAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir a apólice: {ex.Message}");
            }
        }
    }
}
