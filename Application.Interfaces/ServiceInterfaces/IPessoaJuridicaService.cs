using Application.DTO;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IPessoaJuridicaService : IService<PessoaJuridica>
    {
        Task<PessoaJuridicaDto?> FindByCnpj(string cnpj);
        Task<IEnumerable<PessoaJuridicaDto?>> FindByNomeFantasia(string nomeFantasia);
        Task<IEnumerable<PessoaJuridicaDto?>> FindByRazaoSocial(string razaoSocial);
        Task<PessoaJuridicaDto> Create(CreatePessoaJuridicaDto dto);
        Task<UpdatePessoaJuridicaDto?> UpdateById(Guid id, UpdatePessoaJuridicaDto dto);
    }
}