using Application.DTO;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class EnderecoService : BaseService<Endereco>, IEnderecoService
    {
        private readonly ILogger<EnderecoService> _logger;
        private readonly IEnderecoRepository _repository;
        private readonly IViaCepService _viaCepService;

        public EnderecoService(
            ILogger<EnderecoService> logger,
            IEnderecoRepository repository,
            IViaCepService viaCepService
        ) : base(repository)
        {
            _logger = logger;
            _repository = repository;
            _viaCepService = viaCepService;
        }

        public async Task<IEnumerable<EnderecoDto?>> FindByCep(string cep, int page = 1, int limit = 10)
        {
            try
            {
                var enderecos = await _repository.FindByCep(cep, page, limit);

                if (enderecos == null)
                    return null;

                return enderecos.Select(e => new EnderecoDto(
                    e.Id,
                    e.Cep,
                    e.Logradouro,
                    e.Complemento,
                    e.Unidade,
                    e.Bairro,
                    e.Localidade,
                    e.Uf,
                    e.Estado,
                    e.Regiao,
                    e.Ddd
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error to find Endereco by CEP: {cep}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<EnderecoDto?>> FindByLocalidade(string localidade, int page = 1, int limit = 10)
        {
            try
            {
                var enderecos = await _repository.FindByLocalidade(localidade, page, limit);

                if (enderecos == null)
                    return null;

                return enderecos.Select(e => new EnderecoDto(
                    e.Id,
                    e.Cep,
                    e.Logradouro,
                    e.Complemento,
                    e.Unidade,
                    e.Bairro,
                    e.Localidade,
                    e.Uf,
                    e.Estado,
                    e.Regiao,
                    e.Ddd
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error to find Endereco by Localidade: {localidade}", ex);
                throw;
            }
        }

        public async Task<EnderecoDto> Create(CreateEnderecoDto dto)
        {
            try
            {
                var enderecoDto = await _viaCepService.FindAddressByCep(dto.Cep);

                var endereco = new Endereco(
                    cep: enderecoDto.Cep,
                    logradouro: enderecoDto.Logradouro,
                    complemento: enderecoDto.Complemento,
                    unidade: enderecoDto.Unidade ?? "",
                    bairro: enderecoDto.Bairro,
                    localidade: enderecoDto.Localidade,
                    uf: enderecoDto.Uf,
                    estado: enderecoDto.Estado ?? "",
                    regiao: enderecoDto.Regiao ?? "",
                    ddd: enderecoDto.Ddd ?? ""
                );

                await _repository.Create(endereco);

                return new EnderecoDto(
                    endereco.Id,
                    endereco.Cep,
                    endereco.Logradouro,
                    endereco.Complemento,
                    endereco.Unidade,
                    endereco.Bairro,
                    endereco.Localidade,
                    endereco.Uf,
                    endereco.Estado,
                    endereco.Regiao,
                    endereco.Ddd
                );
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error to create endereço entity. payload: {dto}", ex);
                throw;
            }
        }

        public async Task<EnderecoDto?> UpdateById(Guid id, UpdateEnderecoDto dto)
        {
            try
            {
                var endereco = await _repository.FindById(id);
                if (endereco == null)
                    return null;

                endereco.Update(
                    dto.cep,
                    dto.Logradouro,
                    dto.Complemento,
                    dto.Unidade,
                    dto.Bairro,
                    dto.Localidade,
                    dto.Uf,
                    dto.Estado,
                    dto.Regiao,
                    dto.Ddd
                );

                await _repository.UpdateById(id, endereco);

                return new EnderecoDto(
                    endereco.Id,
                    endereco.Cep,
                    endereco.Logradouro,
                    endereco.Complemento,
                    endereco.Unidade,
                    endereco.Bairro,
                    endereco.Localidade,
                    endereco.Uf,
                    endereco.Estado,
                    endereco.Regiao,
                    endereco.Ddd
                );
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error to update endereço entity. payload: {dto}", ex);
                throw;
            }
        }
    }
}