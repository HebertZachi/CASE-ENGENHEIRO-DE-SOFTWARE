using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IPessoaJuridicaService : IService<PessoaJuridica>
    {
        Task<PessoaJuridica?> FindByCnpj(string cnpj);
        Task<IEnumerable<PessoaJuridica>> FindByNomeFantasia(string nomeFantasia);
        Task<IEnumerable<PessoaJuridica>> FindByRazaoSocial(string razaoSocial);
    }
}
