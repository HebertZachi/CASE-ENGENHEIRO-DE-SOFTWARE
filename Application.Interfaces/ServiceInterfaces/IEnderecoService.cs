using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IEnderecoService : IService<Endereco>
    {
        Task<IEnumerable<Endereco>> FindByCep(string cep, int page = 1, int limit = 10);
        Task<IEnumerable<Endereco>> FindByLocalidade(string localidade, int page = 1, int limit = 10);
    }
}
