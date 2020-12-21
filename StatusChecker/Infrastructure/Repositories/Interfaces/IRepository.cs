using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatusChecker.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task<int> SaveAsync(T item);

        Task<int> DeleteAsync(T item);
    }
}
