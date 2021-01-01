using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

using StatusChecker.Infrastructure.Repositories.Interfaces;
using StatusChecker.Models.Database;
using StatusChecker.Services.Interfaces;
using StatusChecker.Models.Enums;
using StatusChecker.Helper;

namespace StatusChecker.Services
{
    public class SettingService : ISettingService
    {
        #region Fields
        private readonly IRepository<Setting> _settingRepository;
        #endregion


        #region Construction
        public SettingService()
        {
            _settingRepository = DependencyService.Get<IRepository<Setting>>();
        }
        #endregion


        #region Interface Methods
        /// <summary>
        /// Get SettingValue by ID
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns></returns>
        public async Task<string> GetSettingValueAsync(int settingId)
        {
            Setting setting = await _settingRepository.GetAsync(settingId);

            if(ValidationHelper.IsSettingValid(setting)) return setting.Value;

            return null;
        }


        /// <summary>
        /// Get SettingValue by Enum
        /// </summary>
        /// <param name="settingKey"></param>
        /// <returns></returns>
        public async Task<string> GetSettingValueAsync(SettingKeys settingKey)
        {
            return await GetSettingValueAsync((int)settingKey);
        }


        /// <summary>
        /// Updates the Value of a defined Setting
        /// </summary>
        /// <param name="settingKey"></param>
        /// <param name="newValue"></param>
        public async void UpdateSettingValue(SettingKeys settingKey, string newValue)
        {
            await _settingRepository.SaveAsync(new Setting
            {
                Id = (int)settingKey,
                Key = settingKey.ToString(),
                Value = newValue
            });
        }


        /// <summary>
        /// Updates Values of multiple Settings
        /// </summary>
        /// <param name="updateSettings"></param>
        public void UpdateSettingsValues(Dictionary<SettingKeys, string> updateSettings)
        {
            foreach(var updateSetting in updateSettings)
            {
                UpdateSettingValue(updateSetting.Key, updateSetting.Value);
            }
        }
        #endregion
    }
}
