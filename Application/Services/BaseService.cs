using Application.Services.Interfaces;
using Infrastructure.Repositories.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class BaseService<T> : IService<T> where T : Entity
    {
        private readonly IGenericRepository<T> _repository;

        public BaseService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual Task<T?> FindById(Guid id) => _repository.FindById(id);

        public virtual Task<IEnumerable<T>> GetAllBylimit(int page = 1, int limit = 10) =>
            _repository.GetAllBylimit(page, limit);

        public virtual Task Create(T entity) => _repository.Create(entity);

        public virtual Task UpdateById(Guid id, T entity) => _repository.UpdateById(id, entity);

        public virtual Task SoftDelete(Guid id) => _repository.SoftDelete(id);
    }
}
