using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class EnderecoRepository : GenericRepository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Endereco>> FindByCep(string cep, int page = 1, int limit = 10)
        {
            if (page < 1) page = 1;
            if (limit < 1) limit = 10;

            return await _dbSet
                .AsNoTracking()
                .Where(e => !e.IsDeleted && e.Cep == cep)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<IEnumerable<Endereco>> FindByLocalidade(string localidade, int page = 1, int limit = 10)
        {
            if (page < 1) page = 1;
            if (limit < 1) limit = 10;

            return await _dbSet
                .AsNoTracking()
                .Where(e => !e.IsDeleted && e.Localidade.Contains(localidade))
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }
    }
}
