using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PessoaFisicaEnderecoRepository : GenericRepository<PessoaFisicaEndereco>, IPessoaFisicaEnderecoRepository
    {
        public PessoaFisicaEnderecoRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<PessoaFisicaEndereco>> FindAllByPessoaFisicaId(Guid pessoaFisicaId, int page = 1, int limit = 10)
        {
            if (page < 1) page = 1;
            if (limit < 1) limit = 10;

            return await _dbSet
                .AsNoTracking()
                .Where(p => !p.IsDeleted && p.PessoaFisicaId == pessoaFisicaId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }
    }
}
