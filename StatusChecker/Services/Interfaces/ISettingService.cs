using System.Collections.Generic;
using System.Threading.Tasks;
using StatusChecker.Models.Enums;

namespace StatusChecker.Services.Interfaces
{
    public interface ISettingService
    {
        /// <summary>
        /// Get the Setting Value by ID
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns></returns>
        Task<string> GetSettingValueAsync(int settingId);

        /// <summary>
        /// Get the Setting Value by Enum
        /// </summary>
        /// <param name="settingKey"></param>
        /// <returns></returns>
        Task<string> GetSettingValueAsync(SettingKeys settingKey);

        /// <summary>
        /// Update a single Setting Item
        /// </summary>
        /// <param name="settingKey"></param>
        /// <param name="newValue"></param>
        void UpdateSettingValue(SettingKeys settingKey, string newValue);

        /// <summary>
        /// Update multiple Setting Items
        /// </summary>
        /// <param name="updateSettings"></param>
        void UpdateSettingsValues(Dictionary<SettingKeys, string> updateSettings);
    }
}
