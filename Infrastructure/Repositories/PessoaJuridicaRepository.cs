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
                .Include(p => p.Endereco)
                .FirstOrDefaultAsync(p => !p.IsDeleted && p.Cnpj == cnpj);
        }

        public async Task<IEnumerable<PessoaJuridica>> FindByRazaoSocial(
            string razaoSocial, int page = 1, int limit = 10)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Endereco)
                .Where(p => !p.IsDeleted && p.RazaoSocial.Contains(razaoSocial))
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<IEnumerable<PessoaJuridica>> FindByNomeFantasia(
            string nomeFantasia, int page = 1, int limit = 10)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Endereco)
                .Where(p => !p.IsDeleted && p.NomeFantasia.Contains(nomeFantasia))
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }
    }
}
