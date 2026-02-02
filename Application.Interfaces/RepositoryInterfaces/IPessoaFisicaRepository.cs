using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IPessoaFisicaRepository : IGenericRepository<PessoaFisica>
    {
        Task<PessoaFisica?> FindByCpf(string cpf);
        Task<IEnumerable<PessoaFisica?>> FindByName(string name, int page = 1, int limit = 10);
    }
}
