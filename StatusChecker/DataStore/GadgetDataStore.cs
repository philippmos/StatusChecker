using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

using StatusChecker.Models.Database;
using StatusChecker.DataStore.Interfaces;
using StatusChecker.Infrastructure.Repositories.Interfaces;

namespace StatusChecker.DataStore
{
    public class GadgetDataStore : IGadgetDataStore
    {
        private readonly IRepository<Gadget> _gadgetRepository = DependencyService.Get<IRepository<Gadget>>();

        public GadgetDataStore() { }

        public async Task<bool> AddAsync(Gadget gadget)
        {
            await _gadgetRepository.SaveAsync(gadget);

            return await Task.FromResult(true);
        }


        public async Task<bool> UpdateAsync(Gadget gadget)
        {
            await _gadgetRepository.SaveAsync(gadget);

            // Gadget updatedGadget = await _gadgetRepository.GetAsync(gadget.Id);

            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            Gadget gadget = await GetAsync(id);

            await _gadgetRepository.DeleteAsync(gadget);

            return true;
        }


        public async Task<Gadget> GetAsync(int id)
        {
            return await _gadgetRepository.GetAsync(id);
        }


        public async Task<IEnumerable<Gadget>> GetAllAsync(bool forceRefresh = false)
        {
            return await _gadgetRepository.GetAllAsync();
        }
    }
}