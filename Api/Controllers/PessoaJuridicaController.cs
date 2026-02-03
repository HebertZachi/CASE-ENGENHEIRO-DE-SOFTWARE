using Application.DTO;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaJuridicaController : ControllerBase
    {
        private readonly IPessoaJuridicaService _pessoaJuridicaService;

        public PessoaJuridicaController(IPessoaJuridicaService pessoaJuridicaService)
        {
            _pessoaJuridicaService = pessoaJuridicaService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePessoaJuridicaDto dto)
        {
            try
            {
                var result = await _pessoaJuridicaService.Create(dto);
                return Created(string.Empty, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> FindById(Guid id)
        {
            try
            {
                var pessoaFisica = await _pessoaJuridicaService.FindById(id);
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
                var pessoas = await _pessoaJuridicaService.GetAllByPage(page, limit);
                return Ok(pessoas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpGet("cnpj/{cnpj}")]
        public async Task<IActionResult> FindByCnpj(string cnpj)
        {
            try
            {
                var pessoa = await _pessoaJuridicaService.FindByCnpj(cnpj);
                if (pessoa == null)
                    return NotFound();

                return Ok(pessoa);

            } catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("nome-fantasia")]
        public async Task<IActionResult> FindByNomeFantasia([FromQuery] string value) 
        {
            try
            {
                var pessoas = await _pessoaJuridicaService.FindByNomeFantasia(value);
                return Ok(pessoas);
            } catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("razao-social")]
        public async Task<IActionResult> FindByRazaoSocial(
            [FromQuery] string value)
        {
            try
            {
                var pessoas = await _pessoaJuridicaService.FindByRazaoSocial(value);
                return Ok(pessoas);
            } catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdatePessoaJuridicaDto dto)
        {
            try
            {
                var updated = await _pessoaJuridicaService.UpdateById(id, dto);
                if (updated == null)
                    return NotFound();

                return Ok(updated);

            } catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _pessoaJuridicaService.SoftDelete(id);
                return NoContent();
            } catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }

}
