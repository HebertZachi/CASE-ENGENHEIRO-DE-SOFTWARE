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
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PessoaFisica>> FindByName(string name, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            return await _dbSet
                .AsNoTracking()
                .Where(p => !p.IsDeleted && p.Nome.Contains(name))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
