using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

using StatusChecker.Models.Database;
using StatusChecker.Infrastructure.Repositories.Interfaces;

namespace StatusChecker.Infrastructure.Repositories
{
    public class SettingRepository : ISettingRepository
    {
        private static readonly Lazy<SQLiteAsyncConnection> _lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(DbConstants.DatabasePath, DbConstants.Flags);
        });

        private static SQLiteAsyncConnection _database => _lazyInitializer.Value;

        private static bool _initialized = false;

        public SettingRepository()
        {
            InitializeAsync().SafeFireAndForget(false);
        }


        private async Task InitializeAsync()
        {
            if (!_initialized)
            {
                if (_database.TableMappings.Any(m => m.MappedType.Name == typeof(Setting).Name))
                {
                    _initialized = true;

                    return;
                }

                await _database.CreateTablesAsync(CreateFlags.None, typeof(Setting)).ConfigureAwait(false);

                InitializeItemsAsync();

                _initialized = true;
            }
        }

        /// <summary>
        /// Setup Setting Initialization for each Setting
        /// </summary>
        private async void InitializeItemsAsync()
        {
            await _database.InsertAsync(new Setting
            {
                Id = (int)SettingKeys.StatusRequestUrl,
                Key = SettingKeys.StatusRequestUrl.ToString(),
                Value = AppSettingsManager.Settings["InitialStatusRequestUrl"]
            });

            await _database.InsertAsync(new Setting
            {
                Id = (int)SettingKeys.PermissionTrackErrors,
                Key = SettingKeys.PermissionTrackErrors.ToString(),
                Value = "0"
            });

            await _database.InsertAsync(new Setting
            {
                Id = (int)SettingKeys.NotifyWhenStatusNotRespond,
                Key = SettingKeys.NotifyWhenStatusNotRespond.ToString(),
                Value = "0"
            });

            await _database.InsertAsync(new Setting
            {
                Id = (int)SettingKeys.RequestTimeoutInSeconds,
                Key = SettingKeys.RequestTimeoutInSeconds.ToString(),
                Value = "10"
            });

            await _database.InsertAsync(new Setting
            {
                Id = (int)SettingKeys.RequestTimeoutInSeconds,
                Key = SettingKeys.RequestTimeoutInSeconds.ToString(),
                Value = "0"
            });
        }


        public Task<List<Setting>> GetAllAsync()
        {
            return _database.Table<Setting>().ToListAsync();
        }


        public Task<Setting> GetAsync(int id)
        {
            return _database.Table<Setting>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }


        public Task<int> SaveAsync(Setting item)
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


        public Task<int> DeleteAsync(Setting item)
        {
            return _database.DeleteAsync(item);
        }
    }
}
