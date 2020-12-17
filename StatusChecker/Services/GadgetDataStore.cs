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
        readonly List<Gadget> gadgets;

        public GadgetDataStore()
        {
            gadgets = new List<Gadget>()
            { };
        }

        public async Task<bool> AddItemAsync(Gadget gadget)
        {
            gadgets.Add(gadget);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Gadget gadget)
        {
            var oldGadget = gadgets.Where((Gadget arg) => arg.Id == gadget.Id).FirstOrDefault();
            gadgets.Remove(oldGadget);
            gadgets.Add(gadget);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldGadget = gadgets.Where((Gadget arg) => arg.Id == id).FirstOrDefault();
            gadgets.Remove(oldGadget);

            return await Task.FromResult(true);
        }

        public async Task<Gadget> GetItemAsync(string id)
        {
            return await Task.FromResult(gadgets.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Gadget>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(gadgets);
        }
    }
}