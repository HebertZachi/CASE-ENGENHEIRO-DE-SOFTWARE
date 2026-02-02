using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IViaCepService
    {
        Task<Endereco?> FindAddressByCep(string cep);
    }
}
