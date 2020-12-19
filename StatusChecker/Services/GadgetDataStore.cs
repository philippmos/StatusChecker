using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using StatusChecker.Models;
using StatusChecker.Services.Interfaces;

namespace StatusChecker.Services
{
    public class GadgetDataStore : IDataStore<Gadget>
    {
        public GadgetDataStore() { }

        public async Task<bool> AddItemAsync(Gadget gadget)
        {
            await App.Database.SaveGadgetAsync(gadget);

            return await Task.FromResult(true);
        }


        public async Task<bool> UpdateItemAsync(Gadget gadget)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> DeleteItemAsync(int id)
        {
            Gadget gadget = await GetItemAsync(id);

            await App.Database.DeleteGadgetAsync(gadget);

            return true;
        }


        public async Task<Gadget> GetItemAsync(int id)
        {
            return await App.Database.GetGadgetAsync(id);
        }


        public async Task<IEnumerable<Gadget>> GetItemsAsync(bool forceRefresh = false)
        {
            return await App.Database.GetGadgetsAsync();
        }
    }
}