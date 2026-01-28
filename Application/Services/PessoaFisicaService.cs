using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class PessoaFisicaService : BaseService<PessoaFisica>, IPessoaFisicaService
    {
        private readonly IPessoaFisicaRepository _repository;

        public PessoaFisicaService(IPessoaFisicaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
        public Task<PessoaFisica?> FindByCpf(string cpf) => _repository.FindByCpf(cpf);

        public Task<IEnumerable<PessoaFisica>> FindByName(string name, int pageNumber = 1, int pageSize = 10) =>
            _repository.FindByName(name, pageNumber, pageSize);
    }
}
