using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.AuthService.Domain.RepositoryContracts
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T> AddAsync(T entity);

        public Task DeleteAsync(T entity);

        public Task<T?> GetByIdAsync(Guid id);

        public Task UpdateAsync(T entity);
    }
}
