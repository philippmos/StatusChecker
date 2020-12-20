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
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(DbConstants.DatabasePath, DbConstants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public GadgetRepository()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Gadget).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Gadget)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }

        public Task<List<Gadget>> GetAllAsync()
        {
            return Database.Table<Gadget>().ToListAsync();
        }


        public Task<Gadget> GetAsync(int id)
        {
            return Database.Table<Gadget>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }


        public Task<int> SaveAsync(Gadget item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }


        public Task<int> DeleteAsync(Gadget item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
