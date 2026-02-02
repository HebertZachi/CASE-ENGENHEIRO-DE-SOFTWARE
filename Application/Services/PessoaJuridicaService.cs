using Application.DTO;
using Application.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;


namespace Application.Services
{
    public class PessoaJuridicaService : BaseService<PessoaJuridica>, IPessoaJuridicaService
    {

        private readonly IPessoaJuridicaRepository _repository;
        private readonly IViaCepService _viaCepService;

        public PessoaJuridicaService(
            IPessoaJuridicaRepository repository,
            IViaCepService viaCepService)
            : base(repository)
        {
            _repository = repository;
            _viaCepService = viaCepService;
        }

        public async Task<PessoaJuridicaDto?> FindByCnpj(string cnpj)
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
        }

        public async Task<IEnumerable<PessoaJuridicaDto?>> FindByNomeFantasia(string nomeFantasia)
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
        }

        public async Task<IEnumerable<PessoaJuridicaDto?>> FindByRazaoSocial(string razaoSocial)
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
        }

        public async Task<PessoaJuridicaDto> Create(CreatePessoaJuridicaDto dto)
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
        }

        public async Task<UpdatePessoaJuridicaDto?> UpdateById(
            Guid id,
            UpdatePessoaJuridicaDto dto)
        {
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
        }

    }
}
