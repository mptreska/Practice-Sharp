using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedClinic.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task AddAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);
        Task SaveAsync();
    }
}