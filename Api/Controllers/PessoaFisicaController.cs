using Application.DTO;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaFisicaController : ControllerBase
    {
        private readonly IPessoaFisicaService _pessoaFisicaService;

        public PessoaFisicaController(IPessoaFisicaService pessoaFisicaService)
        {
            _pessoaFisicaService = pessoaFisicaService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePessoaFisicaDto dto)
        {
            try
            {
                var pessoaFisica = await _pessoaFisicaService.Create(dto);
                return CreatedAtAction(nameof(FindById), new { id = pessoaFisica.Id }, pessoaFisica);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> FindById(Guid id)
        {
            try
            {
                var pessoaFisica = await _pessoaFisicaService.FindById(id);
                if (pessoaFisica == null)
                    return NotFound();

                return Ok(pessoaFisica);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            try
            {
                var pessoas = await _pessoaFisicaService.GetAllBylimit(page, limit);
                return Ok(pessoas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("by-cpf/{cpf}")]
        public async Task<IActionResult> FindByCpf(string cpf)
        {
            try
            {
                var pessoa = await _pessoaFisicaService.FindByCpf(cpf);

                if (pessoa == null)
                    return NotFound();

                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> FindByName(
            [FromQuery] string name,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            try
            {
                var pessoas = await _pessoaFisicaService.FindByName(name, page, limit);
                return Ok(pessoas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePessoaFisicaDto dto)
        {
            try
            {
                await _pessoaFisicaService.UpdateById(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _pessoaFisicaService.SoftDelete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }

}
