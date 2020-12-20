using System.Collections.Generic;
using System.Threading.Tasks;

using StatusChecker.Models.Database;

namespace StatusChecker.Infrastructure.Interfaces
{
    public interface IDatabase<T>
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(int id);

        Task<int> SaveAsync(T item);

        Task<int> DeleteAsync(T item);
    }
}
