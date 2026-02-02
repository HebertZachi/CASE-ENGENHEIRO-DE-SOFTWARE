using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IPessoaFisicaEnderecoService : IService<PessoaFisicaEndereco>
    {
        Task<IEnumerable<PessoaFisicaEndereco>> FindByPessoaFisicaId(Guid pessoaFisicaId, int page = 1, int limit = 10);
    }
}