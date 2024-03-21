using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.OfficesService.Domain.RepositoryContracts
{
    public interface IRepository<T>
    {
        public Task<List<T>> GetAllAsync();

        public Task<T> AddAsync(T entity);

        public Task DeleteAsync(T entity);

        public Task<T?> GetByIdAsync(Guid id);

        public Task UpdateAsync(T entity);
    }
}
