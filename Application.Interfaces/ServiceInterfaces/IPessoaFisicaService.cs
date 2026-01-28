using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IPessoaFisicaService : IService<PessoaFisica>
    {
        Task<PessoaFisica?> FindByCpf(string cpf);
        Task<IEnumerable<PessoaFisica>> FindByName(string name, int pageNumber = 1, int pageSize = 10);
    }
}
