using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IPessoaFisicaEnderecoRepository : IGenericRepository<PessoaFisicaEndereco>
    {
        Task<IEnumerable<PessoaFisicaEndereco>> FindAllByPessoaFisicaId(Guid pessoaFisicaId, int page = 1, int limit = 10);
    }
}
