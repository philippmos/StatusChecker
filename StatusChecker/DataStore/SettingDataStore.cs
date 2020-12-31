using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

using StatusChecker.Models.Database;
using StatusChecker.DataStore.Interfaces;
using StatusChecker.Infrastructure.Repositories.Interfaces;

namespace StatusChecker.DataStore
{
    public class SettingDataStore : ISettingDataStore
    {
        #region Fields
        private readonly IRepository<Setting> _settingRepository;
        #endregion


        #region Construction
        public SettingDataStore() {
            _settingRepository = DependencyService.Get<IRepository<Setting>>();
        }
        #endregion


        #region Interface Methods
        public async Task<bool> AddAsync(Setting setting)
        {
            await _settingRepository.SaveAsync(setting);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Setting setting)
        {
            await _settingRepository.SaveAsync(setting);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Setting setting = await GetAsync(id);

            await _settingRepository.DeleteAsync(setting);

            return true;
        }

        public async Task<Setting> GetAsync(int id)
        {
            return await _settingRepository.GetAsync(id);
        }

        public async Task<IEnumerable<Setting>> GetAllAsync(bool forceRefresh = false)
        {
            return await _settingRepository.GetAllAsync();
        }
        #endregion
    }
}