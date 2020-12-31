using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatusChecker.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Returns all available Items
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Returns a single Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Saves an defined Item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<int> SaveAsync(T item);

        /// <summary>
        /// Removes an defined Item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(T item);
    }
}
