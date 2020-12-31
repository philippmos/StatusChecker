using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

using StatusChecker.Models.Database;
using StatusChecker.Infrastructure.Repositories.Interfaces;

namespace StatusChecker.Infrastructure.Repositories
{
    public class GadgetRepository : IGadgetRepository
    {
        #region Fields
        private static readonly Lazy<SQLiteAsyncConnection> _lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(DbConstants.DatabasePath, DbConstants.Flags);
        });

        private static SQLiteAsyncConnection _database => _lazyInitializer.Value;

        private static bool _initialized = false;
        #endregion


        #region Construction
        public GadgetRepository()
        {
            InitializeAsync().SafeFireAndForget(false);
        }
        #endregion


        #region Initialization
        private async Task InitializeAsync()
        {
            if (!_initialized)
            {
                if (!_database.TableMappings.Any(m => m.MappedType.Name == typeof(Gadget).Name))
                {
                    await _database.CreateTablesAsync(CreateFlags.None, typeof(Gadget)).ConfigureAwait(false);
                }
                _initialized = true;
            }
        }
        #endregion


        #region Repository Methods
        public Task<List<Gadget>> GetAllAsync()
        {
            return _database.Table<Gadget>().ToListAsync();
        }

        public Task<Gadget> GetAsync(int id)
        {
            return _database.Table<Gadget>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveAsync(Gadget item)
        {
            if (item.Id != 0)
            {
                return _database.UpdateAsync(item);
            }
            else
            {
                return _database.InsertAsync(item);
            }
        }


        public Task<int> DeleteAsync(Gadget item)
        {
            return _database.DeleteAsync(item);
        }
        #endregion
    }
}
