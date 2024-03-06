using AutoMapper;
using Domain.Models.DTO.ApoliceSeguroDTO;
using Domain.Models.Entities;
using Infra.Repositories.ApoliceRepo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route(template: "api/v1/apolices")]
    public class ApoliceController : ControllerBase
    {
        private readonly IApoliceRepository _repository;
        private readonly IMapper _mapper;

        public ApoliceController(IApoliceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todas as apólices de seguro.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Apolice> apolices = await _repository.GetAllAsync();
        
            var getApolicesDto = apolices.Select(apolice => _mapper.Map<GetApoliceDto>(apolice));

            return Ok(getApolicesDto);
        }
        
        /// <summary>
        /// Retorna a apólice de seguro com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da apólice de seguro a ser recuperada.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetApoliceDto>> GetById(int id)
        {
            Apolice apolice = await _repository.GetByIdAsync(id);

            if (apolice == null)
            {
                return NotFound();
            }

            GetApoliceDto getApoliceDto = _mapper.Map<GetApoliceDto>(apolice);

            return Ok(getApoliceDto);
        }
        
        /// <summary>
        /// Cria uma nova apólice de seguro.
        /// </summary>
        /// <param name="apolice">Os dados da nova apólice de seguro a ser criada.</param>
        [HttpPost]
        public async Task<IActionResult> Create(Apolice apolice)
        {
            Apolice novaApolice = await _repository.CreateAsync(apolice);

            return CreatedAtAction(nameof(GetById), new { id = apolice.Id }, novaApolice);
        }
        
        /// <summary>
        /// Atualiza a apólice de seguro com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da apólice de seguro a ser atualizada.</param>
        /// <param name="apolice">Os novos dados da apólice de seguro.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update (int id, Apolice apolice)
        {
            var apoliceExistente = await _repository.GetByIdAsync(id);
            if (apoliceExistente == null)
            {
                return NotFound();
            }
        
            await _repository.UpdateAsync(id, apolice);
        
            GetApoliceDto getApoliceDto = _mapper.Map<GetApoliceDto>(apoliceExistente);

            return Ok(getApoliceDto);
        }
        
        /// <summary>
        /// Deleta a apólice de seguro com o ID especificado.
        /// </summary>
        /// <param name="id">O ID da apólice de seguro a ser deletada.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Apolice apoliceParaDeletar = await _repository.GetByIdAsync(id);

            if (apoliceParaDeletar != null)
            {
                await _repository.DeleteAsync(id);
            
                GetApoliceDto getApoliceDto = _mapper.Map<GetApoliceDto>(apoliceParaDeletar);
            
                return Ok(getApoliceDto);
            }

            return NotFound();
        }
    }
}
