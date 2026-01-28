using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IPessoaFisicaEnderecoService : IService<PessoaFisicaEndereco>
    {
        Task<IEnumerable<PessoaFisicaEndereco>> FindByPessoaFisicaId(Guid pessoaFisicaId, int pageNumber = 1, int pageSize = 10);
    }
}