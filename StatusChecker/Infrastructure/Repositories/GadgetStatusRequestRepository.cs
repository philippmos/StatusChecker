using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

using StatusChecker.Models.Database;
using StatusChecker.Infrastructure.Repositories.Interfaces;

namespace StatusChecker.Infrastructure.Repositories
{
    public class GadgetStatusRequestRepository : IGadgetStatusRequestRepository
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
        public GadgetStatusRequestRepository()
        {
            InitializeAsync().SafeFireAndForget(false);
        }
        #endregion


        #region Initialization
        private async Task InitializeAsync()
        {
            if (!_initialized)
            {
                if (!_database.TableMappings.Any(m => m.MappedType.Name == typeof(GadgetStatusRequest).Name))
                {
                    await _database.CreateTablesAsync(CreateFlags.None, typeof(GadgetStatusRequest)).ConfigureAwait(false);
                }
                _initialized = true;
            }
        }
        #endregion


        #region Repository Methods
        #region IRepository
        public Task<List<GadgetStatusRequest>> GetAllAsync()
        {
            return _database.Table<GadgetStatusRequest>().ToListAsync();
        }

        public Task<GadgetStatusRequest> GetAsync(int id)
        {
            return _database.Table<GadgetStatusRequest>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveAsync(GadgetStatusRequest item)
        {
            if (item.Id != 0)
            {
                throw new NotImplementedException();
            }
            else
            {
                return _database.InsertAsync(item);
            }
        }


        public Task<int> DeleteAsync(GadgetStatusRequest item)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region IGadgetStatusRepository
        public Task<List<GadgetStatusRequest>> GetAllValidStatusRequestsForGadgetIdAsync(int gadgetId)
        {
            return _database.Table<GadgetStatusRequest>()
                                .Where(x => (x.GadgetId == gadgetId) && (x.IsStatusRequestValid == true))
                                .ToListAsync();

        }
        #endregion
        #endregion
    }
}
