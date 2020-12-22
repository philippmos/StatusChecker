using Xamarin.Forms;

using System.Threading.Tasks;
using StatusChecker.Infrastructure.Repositories.Interfaces;
using StatusChecker.Models.Database;
using StatusChecker.Services.Interfaces;

namespace StatusChecker.Services
{
    public class SettingService : ISettingService
    {
        private readonly IRepository<Setting> _settingRepository;

        public SettingService()
        {
            _settingRepository = DependencyService.Get<IRepository<Setting>>();
        }

        /// <summary>
        /// Get SettingValue by ID
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns></returns>
        public async Task<string> GetSettingValueAsync(int settingId)
        {
            Setting setting = await _settingRepository.GetAsync(settingId);

            if(IsSettingValid(setting)) return null;

            return setting.Value;
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
        /// Checks if Setting is valid
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        private bool IsSettingValid(Setting setting)
        {
            return setting != null && !string.IsNullOrEmpty(setting.Value);
        }
    }
}
