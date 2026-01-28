using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task<T?> FindById(Guid id);
        Task<IEnumerable<T>> GetAllByPage(int pageNumber = 1, int pageSize = 10);
        Task Create(T entity);
        Task UpdateById(Guid id, T updatedEntity);
        Task SoftDelete(Guid id);
    }
}
