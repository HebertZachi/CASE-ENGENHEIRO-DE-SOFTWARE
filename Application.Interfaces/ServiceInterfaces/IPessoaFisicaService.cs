using Application.DTO;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IPessoaFisicaService : IService<PessoaFisica>
    {
        Task<PessoaFisicaDto?> FindByCpf(string cpf);
        Task<IEnumerable<PessoaFisicaDto?>> FindByName(string name, int page = 1, int limit = 10);
        Task<PessoaFisicaDto> Create(CreatePessoaFisicaDto dto);
        Task<PessoaFisicaDto?> UpdateById(Guid id, UpdatePessoaFisicaDto dto);
    }
}
