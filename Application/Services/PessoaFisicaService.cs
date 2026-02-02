using Application.DTO;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class PessoaFisicaService : BaseService<PessoaFisica>, IPessoaFisicaService
    {
        private readonly IPessoaFisicaRepository _repository;
        private readonly IViaCepService _viaCepService;

        public PessoaFisicaService(
            IPessoaFisicaRepository repository,
            IViaCepService viaCepService)
            : base(repository)
        {
            _repository = repository;
            _viaCepService = viaCepService;
        }

        public PessoaFisicaService(IPessoaFisicaRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
        public async Task<PessoaFisicaDto?> FindByCpf(string cpf)
        {
            var pessoaFisica = await _repository.FindByCpf(cpf);

            if (pessoaFisica == null)
                return null;

            var enderecosDto = pessoaFisica.Enderecos
                .Select(pe => new EnderecoDto(
                    pe.Endereco.Id,
                    pe.Endereco.Cep,
                    pe.Endereco.Logradouro,
                    pe.Endereco.Complemento,
                    pe.Endereco.Unidade,
                    pe.Endereco.Bairro,
                    pe.Endereco.Localidade,
                    pe.Endereco.Uf,
                    pe.Endereco.Estado,
                    pe.Endereco.Regiao,
                    pe.Endereco.Ddd
                ))
                .ToList();

            return new PessoaFisicaDto(
                pessoaFisica.Id,
                pessoaFisica.Nome,
                pessoaFisica.Sobrenome,
                pessoaFisica.Cpf,
                pessoaFisica.DataNascimento,
                enderecosDto
            );
        }

        public async Task<IEnumerable<PessoaFisicaDto?>> FindByName(string name, int page = 1, int limit = 10)
        {
            var pessoasFisica = await _repository.FindByName(name, page, limit);

            if (pessoasFisica == null)
                return null;

            return pessoasFisica.Select(p => new PessoaFisicaDto(
                p.Id,
                p.Nome,
                p.Sobrenome,
                p.Cpf,
                p.DataNascimento,
                p.Enderecos.Select(e => new EnderecoDto(
                    e.Endereco.Id,
                    e.Endereco.Cep,
                    e.Endereco.Logradouro,
                    e.Endereco.Complemento,
                    e.Endereco.Unidade,
                    e.Endereco.Bairro,
                    e.Endereco.Localidade,
                    e.Endereco.Uf,
                    e.Endereco.Estado,
                    e.Endereco.Regiao,
                    e.Endereco.Ddd
                )).ToList()
            ));
        }

        public async Task<PessoaFisicaDto> Create(CreatePessoaFisicaDto dto)
        {
            var pessoaFisica = new PessoaFisica(
                dto.Nome,
                dto.Sobrenome,
                dto.Cpf,
                dto.DataNascimento
            );

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

            var pessoaEndereco = new PessoaFisicaEndereco(
                pessoaFisica,
                endereco
            );

            pessoaFisica.Enderecos.Add(pessoaEndereco);
            await _repository.Create(pessoaFisica);

            return new PessoaFisicaDto(
                pessoaFisica.Id,
                pessoaFisica.Nome,
                pessoaFisica.Sobrenome,
                pessoaFisica.Cpf,
                pessoaFisica.DataNascimento,
                pessoaFisica.Enderecos.Select(e => new EnderecoDto(
                    e.Endereco.Id,
                    e.Endereco.Cep,
                    e.Endereco.Logradouro,
                    e.Endereco.Complemento,
                    e.Endereco.Unidade,
                    e.Endereco.Bairro,
                    e.Endereco.Localidade,
                    e.Endereco.Uf,
                    e.Endereco.Estado,
                    e.Endereco.Regiao,
                    e.Endereco.Ddd
                )).ToList()
            );
        }

        public async Task<PessoaFisicaDto?> UpdateById(Guid id, UpdatePessoaFisicaDto dto)
        {
            var pessoaFisica = await _repository.FindById(id);
            if (pessoaFisica == null) return null;

            pessoaFisica.Update(
                dto.Nome,
                dto.Sobrenome,
                dto.DataNascimento,
                dto.Cpf
            );

            await _repository.UpdateById(id, pessoaFisica);

            return new PessoaFisicaDto(
                pessoaFisica.Id,
                pessoaFisica.Nome,
                pessoaFisica.Sobrenome,
                pessoaFisica.Cpf,
                pessoaFisica.DataNascimento,
                pessoaFisica.Enderecos.Select(e => new EnderecoDto(
                    e.Endereco.Id,
                    e.Endereco.Cep,
                    e.Endereco.Logradouro,
                    e.Endereco.Complemento,
                    e.Endereco.Unidade,
                    e.Endereco.Bairro,
                    e.Endereco.Localidade,
                    e.Endereco.Uf,
                    e.Endereco.Estado,
                    e.Endereco.Regiao,
                    e.Endereco.Ddd
                )).ToList()
            );
        }
    }
}