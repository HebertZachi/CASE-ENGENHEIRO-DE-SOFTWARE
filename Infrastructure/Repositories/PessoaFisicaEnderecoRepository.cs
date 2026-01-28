using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PessoaFisicaEnderecoRepository : GenericRepository<PessoaFisicaEndereco>, IPessoaFisicaEnderecoRepository
    {
        public PessoaFisicaEnderecoRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<PessoaFisicaEndereco>> FindAllByPessoaFisicaId(Guid pessoaFisicaId, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            return await _dbSet
                .AsNoTracking()
                .Where(p => !p.IsDeleted && p.PessoaFisicaId == pessoaFisicaId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
