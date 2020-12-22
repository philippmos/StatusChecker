using System.Threading.Tasks;
using StatusChecker.Models.Database;

namespace StatusChecker.Services.Interfaces
{
    public interface ISettingService
    {
        Task<string> GetSettingValueAsync(int settingId);
        Task<string> GetSettingValueAsync(SettingKeys settingKey);
    }
}
