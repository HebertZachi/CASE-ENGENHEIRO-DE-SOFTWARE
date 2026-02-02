using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public record CreatePessoaFisicaDto(
        [Required, MaxLength(50)] string Nome,
        [Required, MaxLength(50)] string Sobrenome,
        [Required, StringLength(11, MinimumLength = 11)] string Cpf,
        [Required] DateTime DataNascimento,
        [Required, StringLength(8, MinimumLength = 8)] string Cep
    );

    public record UpdatePessoaFisicaDto(
        [Required, MaxLength(50)] string Nome,
        [Required, MaxLength(50)] string Sobrenome,
        [Required, StringLength(11, MinimumLength = 11)] string Cpf,
        [Required] DateTime DataNascimento
    );

    public record PessoaFisicaDto(
        Guid Id,
        string Nome,
        string Sobrenome,
        string Cpf,
        DateTime DataNascimento,
        List<EnderecoDto> Enderecos
    );
}
