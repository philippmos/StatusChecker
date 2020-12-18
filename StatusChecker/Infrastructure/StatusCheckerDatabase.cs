using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

using StatusChecker.Models;

namespace StatusChecker.Infrastructure
{
    public class StatusCheckerDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(DbConstants.DatabasePath, DbConstants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public StatusCheckerDatabase()
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

        public Task<List<Gadget>> GetGadgetsAsync()
        {
            return Database.Table<Gadget>().ToListAsync();
        }


        public Task<Gadget> GetGadgetAsync(int id)
        {
            return Database.Table<Gadget>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }


        public Task<int> SaveGadgetAsync(Gadget gadget)
        {
            if (gadget.Id != 0)
            {
                return Database.UpdateAsync(gadget);
            }
            else
            {
                return Database.InsertAsync(gadget);
            }
        }


        public Task<int> DeleteGadgetAsync(Gadget gadget)
        {
            return Database.DeleteAsync(gadget);
        }
    }
}
