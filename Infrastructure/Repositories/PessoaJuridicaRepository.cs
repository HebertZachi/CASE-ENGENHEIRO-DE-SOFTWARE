using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class PessoaJuridicaRepository : GenericRepository<PessoaJuridica>, IPessoaJuridicaRepository
    {
        public PessoaJuridicaRepository(AppDbContext context) : base(context) { }

        public async Task<PessoaJuridica?> FindByCnpj(string cnpj)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(p => !p.IsDeleted && p.Cnpj == cnpj)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PessoaJuridica>> FindByRazaoSocial(string razaoSocial, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            return await _dbSet
                .AsNoTracking()
                .Where(p => !p.IsDeleted && p.RazaoSocial.Contains(razaoSocial))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<PessoaJuridica>> FindByNomeFantasia(string nomeFantasia, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            return await _dbSet
                .AsNoTracking()
                .Where(p => !p.IsDeleted && p.NomeFantasia.Contains(nomeFantasia))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
