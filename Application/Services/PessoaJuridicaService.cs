using Application.Services.Interfaces;
using Infrastructure.Repositories.Interfaces;
using Domain.Entities;


namespace Application.Services
{
    public class PessoaJuridicaService : BaseService<PessoaJuridica>, IPessoaJuridicaService
    {
        private readonly IPessoaJuridicaRepository _repository;

        public PessoaJuridicaService(IPessoaJuridicaRepository repository, IGenericRepository<PessoaJuridica> genericRepository)
            : base(genericRepository)
        {
            _repository = repository;
        }

        public async Task<PessoaJuridica?> FindByCnpj(string cnpj)
        {
            return await _repository.FindByCnpj(cnpj);
        }
        public async Task<IEnumerable<PessoaJuridica>> FindByRazaoSocial(string razaoSocial)
        {
            return await _repository.FindByRazaoSocial(razaoSocial);
        }

        public async Task<IEnumerable<PessoaJuridica>> FindByNomeFantasia(string nomeFantasia)
        {
            return await _repository.FindByNomeFantasia(nomeFantasia);
        }
    }
}
