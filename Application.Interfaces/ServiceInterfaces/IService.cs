using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IService<T> where T : Entity
    {
        Task<T?> FindById(Guid id);
        Task<IEnumerable<T>> GetAllByPage(int pageNumber = 1, int pageSize = 10);
        Task Create(T entity);
        Task UpdateById(Guid id, T entity);
        Task SoftDelete(Guid id);
    }
}
