using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IPessoaJuridicaRepository : IGenericRepository<PessoaJuridica>
    {
        Task<PessoaJuridica?> FindByCnpj(string cnpj);
        Task<IEnumerable<PessoaJuridica>> FindByNomeFantasia(string nomeFantasia, int page = 1, int limit = 10);
        Task<IEnumerable<PessoaJuridica>> FindByRazaoSocial(string razaoSocial, int page = 1, int limit = 10);
    }
}