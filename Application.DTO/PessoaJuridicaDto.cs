using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public record CreatePessoaJuridicaDto(
        [Required, MaxLength(100)] string RazaoSocial,
        [Required, MaxLength(100)] string NomeFantasia,
        [Required, StringLength(14, MinimumLength = 14)] string Cnpj,
        [Required, StringLength(8, MinimumLength = 8)] string Cep
    );

    public record UpdatePessoaJuridicaDto(
    [Required, MaxLength(100)] string RazaoSocial,
    [Required, MaxLength(100)] string NomeFantasia,
    [Required, StringLength(14, MinimumLength = 14)] string Cnpj
    );

    public record PessoaJuridicaDto(
    Guid Id,
    [Required, MaxLength(100)] string RazaoSocial,
    [Required, MaxLength(100)] string NomeFantasia,
    [Required, StringLength(14, MinimumLength = 14)] string Cnpj,
    EnderecoDto Endereco
    );
}
