using System.Collections.Generic;
using System.Threading.Tasks;

using StatusChecker.Models.Database;
using StatusChecker.DataStore.Interfaces;

namespace StatusChecker.DataStore
{
    public class GadgetDataStore : IDataStore<Gadget>
    {
        public GadgetDataStore() { }

        public async Task<bool> AddItemAsync(Gadget gadget)
        {
            await App.GadgetRepository.SaveAsync(gadget);

            return await Task.FromResult(true);
        }


        public async Task<bool> UpdateItemAsync(Gadget gadget)
        {
            await App.GadgetRepository.SaveAsync(gadget);

            Gadget updatedGadget = await App.GadgetRepository.GetAsync(gadget.Id);

            return true;
        }


        public async Task<bool> DeleteItemAsync(int id)
        {
            Gadget gadget = await GetItemAsync(id);

            await App.GadgetRepository.DeleteAsync(gadget);

            return true;
        }


        public async Task<Gadget> GetItemAsync(int id)
        {
            return await App.GadgetRepository.GetAsync(id);
        }


        public async Task<IEnumerable<Gadget>> GetItemsAsync(bool forceRefresh = false)
        {
            return await App.GadgetRepository.GetAllAsync();
        }
    }
}