using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IPessoaJuridicaRepository : IGenericRepository<PessoaJuridica>
    {
        Task<PessoaJuridica?> FindByCnpj(string cnpj);
        Task<IEnumerable<PessoaJuridica>> FindByNomeFantasia(string nomeFantasia, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<PessoaJuridica>> FindByRazaoSocial(string razaoSocial, int pageNumber = 1, int pageSize = 10);
    }
}