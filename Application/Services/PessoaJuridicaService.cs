using Application.DTO;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;


namespace Application.Services
{
    public class PessoaJuridicaService : BaseService<PessoaJuridica>, IPessoaJuridicaService
    {

        private readonly ILogger<PessoaJuridica> _logger;
        private readonly IPessoaJuridicaRepository _repository;
        private readonly IViaCepService _viaCepService;

        public PessoaJuridicaService(
            ILogger<PessoaJuridica> logger,
            IPessoaJuridicaRepository repository,
            IViaCepService viaCepService)
            : base(repository)
        {
            _logger = logger;
            _repository = repository;
            _viaCepService = viaCepService;
        }

        public async Task<PessoaJuridicaDto?> FindByCnpj(string cnpj)
        {
            try
            {
                var pessoaJuridica = await _repository.FindByCnpj(cnpj);

                if (pessoaJuridica == null)
                    return null;

                return new PessoaJuridicaDto(
                    pessoaJuridica.Id,
                    pessoaJuridica.RazaoSocial,
                    pessoaJuridica.NomeFantasia,
                    pessoaJuridica.Cnpj,
                    new EnderecoDto(
                        pessoaJuridica.Endereco.Id,
                        pessoaJuridica.Endereco.Cep,
                        pessoaJuridica.Endereco.Logradouro,
                        pessoaJuridica.Endereco.Complemento,
                        pessoaJuridica.Endereco.Unidade,
                        pessoaJuridica.Endereco.Bairro,
                        pessoaJuridica.Endereco.Localidade,
                        pessoaJuridica.Endereco.Uf,
                        pessoaJuridica.Endereco.Estado,
                        pessoaJuridica.Endereco.Regiao,
                        pessoaJuridica.Endereco.Ddd
                    )
                );
            } catch (Exception ex)
            {
                _logger.LogError($"Error to find Pessoa Jurídica by CNPJ: {cnpj}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<PessoaJuridicaDto?>> FindByNomeFantasia(string nomeFantasia)
        {
            try
            {
                var pessoas = await _repository.FindByNomeFantasia(nomeFantasia);

                if (pessoas == null)
                    return null;

                return pessoas.Select(p => new PessoaJuridicaDto(
                    p.Id,
                    p.RazaoSocial,
                    p.NomeFantasia,
                    p.Cnpj,
                    new EnderecoDto(
                        p.Endereco.Id,
                        p.Endereco.Cep,
                        p.Endereco.Logradouro,
                        p.Endereco.Complemento,
                        p.Endereco.Unidade,
                        p.Endereco.Bairro,
                        p.Endereco.Localidade,
                        p.Endereco.Uf,
                        p.Endereco.Estado,
                        p.Endereco.Regiao,
                        p.Endereco.Ddd
                    )
                ));
            } catch (Exception ex)
            {
                _logger.LogError($"Error to find Pessoa Jurídica by nome fantasia: {nomeFantasia}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<PessoaJuridicaDto?>> FindByRazaoSocial(string razaoSocial)
        {
            try
            {
                var pessoas = await _repository.FindByRazaoSocial(razaoSocial);

                if (pessoas == null)
                    return null;

                return pessoas.Select(p => new PessoaJuridicaDto(
                    p.Id,
                    p.RazaoSocial,
                    p.NomeFantasia,
                    p.Cnpj,
                    new EnderecoDto(
                        p.Endereco.Id,
                        p.Endereco.Cep,
                        p.Endereco.Logradouro,
                        p.Endereco.Complemento,
                        p.Endereco.Unidade,
                        p.Endereco.Bairro,
                        p.Endereco.Localidade,
                        p.Endereco.Uf,
                        p.Endereco.Estado,
                        p.Endereco.Regiao,
                        p.Endereco.Ddd
                    )
                ));

            } catch (Exception ex)
            {
                _logger.LogError($"Error to find Pessoa Jurídica by razão social: {razaoSocial}", ex);
                throw;
            }
        }

        public async Task<PessoaJuridicaDto> Create(CreatePessoaJuridicaDto dto)
        {
            try
            {
                var existing = await _repository.FindByCnpj(dto.Cnpj);
                if (existing != null)
                    throw new Exception($"CNPJ {dto.Cnpj} já cadastrado.");

                var enderecoDto = await _viaCepService.FindAddressByCep(dto.Cep);
                if (enderecoDto == null)
                    throw new Exception($"CEP não encontrado: {dto.Cep}");

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

                var pessoaJuridica = new PessoaJuridica(
                    razaoSocial: dto.RazaoSocial,
                    nomeFantasia: dto.NomeFantasia,
                    cnpj: dto.Cnpj,
                    endereco: endereco
                );

                await _repository.Create(pessoaJuridica);

                return new PessoaJuridicaDto(
                    pessoaJuridica.Id,
                    pessoaJuridica.RazaoSocial,
                    pessoaJuridica.NomeFantasia,
                    pessoaJuridica.Cnpj,
                    new EnderecoDto(
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
                    )
                );
            } catch (Exception ex)
            {
                _logger.LogError($"Error to create Pessoa Jurídica. Payload: {dto}", ex);
                throw;
            }
        }

        public async Task<UpdatePessoaJuridicaDto?> UpdateById(Guid id,UpdatePessoaJuridicaDto dto)
        {
            try {
                var pessoaJuridica = await _repository.FindById(id);
                if (pessoaJuridica == null)
                    return null;

                pessoaJuridica.Update(
                    dto.RazaoSocial,
                    dto.NomeFantasia,
                    dto.Cnpj
                );

                await _repository.UpdateById(id, pessoaJuridica);

                return new UpdatePessoaJuridicaDto(
                    pessoaJuridica.RazaoSocial,
                    pessoaJuridica.NomeFantasia,
                    pessoaJuridica.Cnpj
                );
            } catch (Exception ex)
            {
                _logger.LogError($"Error to update Pessoa Jurídica id: {id}. Payload: {dto}", ex);
                throw;
            }
        }
    }
}
