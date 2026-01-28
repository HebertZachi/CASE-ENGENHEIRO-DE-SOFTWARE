using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IEnderecoService : IService<Endereco>
    {
        Task<IEnumerable<Endereco>> FindByCep(string cep, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<Endereco>> FindByLocalidade(string localidade, int pageNumber = 1, int pageSize = 10);
    }
}
