using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public record EnderecoDto(
        Guid Id,
        string Cep,
        string Logradouro,
        string Complemento,
        string Unidade,
        string Bairro,
        string Localidade,
        string Uf,
        string Estado,
        string Regiao,
        string Ddd
    );

    public record CreateEnderecoDto(
        [Required, StringLength(8, MinimumLength = 8)] string Cep
    );

    public record UpdateEnderecoDto(
        [Required, MaxLength(150)] string Logradouro,
        [MaxLength(50)] string Complemento,
        [MaxLength(20)] string Unidade,
        [MaxLength(100)] string Bairro,
        [MaxLength(100)] string Localidade,
        [MaxLength(2)] string Uf,
        [MaxLength(50)] string Estado,
        [MaxLength(50)] string Regiao,
        [MaxLength(2)] string Ddd
    );
}
