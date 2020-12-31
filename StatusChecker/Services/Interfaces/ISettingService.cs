using System.Collections.Generic;
using System.Threading.Tasks;
using StatusChecker.Models.Enums;

namespace StatusChecker.Services.Interfaces
{
    public interface ISettingService
    {
        Task<string> GetSettingValueAsync(int settingId);
        Task<string> GetSettingValueAsync(SettingKeys settingKey);

        void UpdateSettingValue(SettingKeys settingKey, string newValue);
        void UpdateSettingsValues(Dictionary<SettingKeys, string> updateSettings);
    }
}
