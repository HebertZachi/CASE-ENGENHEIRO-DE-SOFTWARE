using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IService<T> where T : Entity
    {
        Task<T?> FindById(Guid id);
        Task<IEnumerable<T>> GetAllBylimit(int page = 1, int limit = 10);
        Task SoftDelete(Guid id);
    }
}
