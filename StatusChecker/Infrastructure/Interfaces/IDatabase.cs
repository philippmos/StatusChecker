using System.Collections.Generic;
using System.Threading.Tasks;

using StatusChecker.Models.Database;

namespace StatusChecker.Infrastructure.Interfaces
{
    public interface IDatabase
    {
        Task<List<Gadget>> GetGadgetsAsync();

        Task<Gadget> GetGadgetAsync(int id);

        Task<int> SaveGadgetAsync(Gadget gadget);

        Task<int> DeleteGadgetAsync(Gadget gadget);
    }
}
