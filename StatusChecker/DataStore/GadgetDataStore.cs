using System.Collections.Generic;
using System.Threading.Tasks;

using StatusChecker.Models.Database;
using StatusChecker.DataStore.Interfaces;

namespace StatusChecker.DataStore
{
    public class GadgetDataStore : IGadgetDataStore
    {
        public GadgetDataStore() { }

        public async Task<bool> AddAsync(Gadget gadget)
        {
            await App.GadgetRepository.SaveAsync(gadget);

            return await Task.FromResult(true);
        }


        public async Task<bool> UpdateAsync(Gadget gadget)
        {
            await App.GadgetRepository.SaveAsync(gadget);

            // Gadget updatedGadget = await App.GadgetRepository.GetAsync(gadget.Id);

            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            Gadget gadget = await GetAsync(id);

            await App.GadgetRepository.DeleteAsync(gadget);

            return true;
        }


        public async Task<Gadget> GetAsync(int id)
        {
            return await App.GadgetRepository.GetAsync(id);
        }


        public async Task<IEnumerable<Gadget>> GetAllAsync(bool forceRefresh = false)
        {
            return await App.GadgetRepository.GetAllAsync();
        }
    }
}