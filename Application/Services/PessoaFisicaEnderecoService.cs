using Application.Services.Interfaces;
using Infrastructure.Repositories.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class PessoaFisicaEnderecoService : BaseService<PessoaFisicaEndereco>, IPessoaFisicaEnderecoService
    {
        private readonly IPessoaFisicaEnderecoRepository _repository;

        public PessoaFisicaEnderecoService(IPessoaFisicaEnderecoRepository repository, IGenericRepository<PessoaFisicaEndereco> genericRepository)
            : base(genericRepository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PessoaFisicaEndereco>> FindByPessoaFisicaId(Guid pessoaFisicaId, int page = 1, int limit = 10)
        {
            return await _repository.FindAllByPessoaFisicaId(pessoaFisicaId, page, limit);
        }
    }
}
