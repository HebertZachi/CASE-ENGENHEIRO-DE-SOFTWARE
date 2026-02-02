using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PessoaFisicaRepository : GenericRepository<PessoaFisica>, IPessoaFisicaRepository
    {
        public PessoaFisicaRepository(AppDbContext context) : base(context) { }

        public async Task<PessoaFisica?> FindByCpf(string cpf)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(p => !p.IsDeleted && p.Cpf == cpf)
                .Include(p => p.Enderecos)
                    .ThenInclude(pe => pe.Endereco)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PessoaFisica>> FindByName(string name, int page = 1, int limit = 10)
        {
            if (page < 1) page = 1;
            if (limit < 1) limit = 10;

            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Enderecos)
                    .ThenInclude(pe => pe.Endereco)
                .Where(p => !p.IsDeleted && p.Nome.Contains(name))
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }
    }
}