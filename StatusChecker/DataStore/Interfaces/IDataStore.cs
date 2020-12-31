using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatusChecker.DataStore.Interfaces
{
    public interface IDataStore<T>
    {
        /// <summary>
        /// Get all Items in App-DataStore
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(bool forceRefresh = false);

        /// <summary>
        /// Get a specific Item from App-DataStore
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Add Item to App-DataStore
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> AddAsync(T item);

        /// <summary>
        /// Updates a specific Item in App-DataStore
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T item);

        /// <summary>
        /// Remove a specific Item from App-DataStore
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);
    }
}
