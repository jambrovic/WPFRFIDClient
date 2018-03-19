using RFIDClient.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Service
{
    class ItemRepositoryService : IRepositoryService<ItemService>
    {
        public async Task<long> Delete(string id)
        {
            return await ItemFactory.GetInstance().DeleteAsync(id);
        }

        public async Task Insert(ItemService entity)
        {
            await ItemFactory.GetInstance().InsertAsync(EntityConverter.GetItem(entity));
        }

        public async Task<ItemService> Select(string id)
        {
            return EntityConverter.GetItem(await ItemFactory.GetInstance().SelectAsync(id));
        }

        public async Task<List<ItemService>> SelectAll()
        {
            return EntityConverter.GetItems(await ItemFactory.GetInstance().SelectAllAsync());
        }

        public async Task<long> Update(ItemService entity)
        {
            return await ItemFactory.GetInstance().UpdateAsync(EntityConverter.GetItem(entity));
        }
    }
}
