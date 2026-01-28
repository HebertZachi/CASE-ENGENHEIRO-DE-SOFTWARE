using Application.Services.Interfaces;
using Infrastructure.Repositories.Interfaces;
using Domain.Entities;


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

        public async Task<IEnumerable<Endereco>> FindByCep(string cep, int pageNumber = 1, int pageSize = 10) => 
            await _repository.FindByCep(cep, pageNumber, pageSize);

        public async Task<IEnumerable<Endereco>> FindByLocalidade(string localidade, int pageNumber = 1, int pageSize = 10) =>
            await _repository.FindByLocalidade(localidade, pageNumber, pageSize);
    }
}
