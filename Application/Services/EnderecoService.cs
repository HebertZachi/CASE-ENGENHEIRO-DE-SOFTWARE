using Application.DTO;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;


namespace Application.Services
{
    public class EnderecoService : BaseService<Endereco>, IEnderecoService
    {
        private readonly IEnderecoRepository _repository;
        public EnderecoService(IEnderecoRepository repository, IGenericRepository<Endereco> genericRepository)
            : base(genericRepository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Endereco>> FindByCep(string cep, int page = 1, int limit = 10) => 
            await _repository.FindByCep(cep, page, limit);

        public async Task<IEnumerable<Endereco>> FindByLocalidade(string localidade, int page = 1, int limit = 10) =>
            await _repository.FindByLocalidade(localidade, page, limit);
    }
}
