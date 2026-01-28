using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IEnderecoRepository : IGenericRepository<Endereco>
    {
        Task<IEnumerable<Endereco>> FindByCep(string cep, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<Endereco>> FindByLocalidade(string localidade, int pageNumber = 1, int pageSize = 10);
    }
}
