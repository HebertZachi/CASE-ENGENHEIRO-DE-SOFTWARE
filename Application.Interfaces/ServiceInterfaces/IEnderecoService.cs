using Application.DTO;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IEnderecoService : IService<Endereco>
    {
        Task<IEnumerable<EnderecoDto>> FindByCep(string cep, int page = 1, int limit = 10);
        Task<IEnumerable<EnderecoDto>> FindByLocalidade(string localidade, int page = 1, int limit = 10);
        Task<EnderecoDto> Create(CreateEnderecoDto dto);
        Task<EnderecoDto?> UpdateById(Guid id, UpdateEnderecoDto dto);
    }
}
