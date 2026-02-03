using Application.DTO;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;

        public EnderecoController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEnderecoDto dto)
        {
            try
            {
                var result = await _enderecoService.Create(dto);
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
                var pessoaFisica = await _enderecoService.FindById(id);
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
                var pessoas = await _enderecoService.GetAllByPage(page, limit);
                return Ok(pessoas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("cep/{cep}")]
        public async Task<IActionResult> FindByCep(string cep, [FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            try
            {
                var enderecos = await _enderecoService.FindByCep(cep, page, limit);

                if (enderecos == null || !enderecos.Any())
                    return NotFound();

                return Ok(enderecos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("localidade/{localidade}")]
        public async Task<IActionResult> FindByLocalidade(
            string localidade,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            try
            {
                var enderecos = await _enderecoService.FindByLocalidade(localidade, page, limit);

                if (enderecos == null || !enderecos.Any())
                    return NotFound();

                return Ok(enderecos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateEnderecoDto dto)
        {
            try
            {
                var updated = await _enderecoService.UpdateById(id, dto);
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
                await _enderecoService.SoftDelete(id);
                return NoContent();
            } catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }

}
