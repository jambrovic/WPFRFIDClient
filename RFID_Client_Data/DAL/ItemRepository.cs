using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RFIDClient.Data
{
    /// <summary>
    /// Class for CRUD operations on Item collection
    /// </summary>
    class ItemRepository : IRepository<DALItem>
    {
        #region Constructor

        public ItemRepository() { }

        #endregion

        #region Public Functions

        /// <summary>
        /// Select a single item
        /// </summary>
        /// <param name="rfid">RFID string of the item</param>
        /// <returns></returns>
        public async Task<DALItem> SelectAsync(string rfid)
        {
            //try
            //{
                var builder = Builders<DALItem>.Filter;
                var filter = builder.Eq("rfidCode", rfid);
                return await DBMongo.ItemsCollection.Find(filter).SingleAsync();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        /// <summary>
        /// Insert a single item
        /// </summary>
        /// <param name="item">The instance of the item object for insert</param>
        /// <returns></returns>
        public async Task InsertAsync(DALItem item)
        {
            try
            {
                #region Delete item if exists

                var builder = Builders<DALItem>.Filter;
                var filter = builder.Eq("rfidCode", item.RFIDCode);
                DeleteResult deleteResult = await DBMongo.ItemsCollection.DeleteManyAsync(filter);

                #endregion

                await DBMongo.ItemsCollection.InsertOneAsync(item);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete a single item
        /// </summary>
        /// <param name="rfid">RFID code of the item for delete</param>
        /// <returns>Count of deleted documents</returns>
        public async Task<long> DeleteAsync(string rfid)
        {
            try
            {
                var builder = Builders<DALItem>.Filter;
                var filter = builder.Eq("rfidCode", rfid);
                DeleteResult result = await DBMongo.ItemsCollection.DeleteManyAsync(filter);
                return result.DeletedCount;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Update a single item by RFID code
        /// </summary>
        /// <param name="newItem">The instance of the updated item</param>
        /// <returns>Returns a number of the updated items</returns>
        public async Task<long> UpdateAsync(DALItem newItem)
        {
            try
            {
                DALItem itemForUpdate = await SelectAsync(newItem.RFIDCode);

                var builder = Builders<DALItem>.Filter;
                var filter = builder.Eq("_id", itemForUpdate.Id);
                var update = Builders<DALItem>.Update
                    .Set("barcode", newItem.Barcode)
                    .Set("name", newItem.Name)
                    .Set("secondaryCode", newItem.SecondaryCode)
                    .Set("unitPrice", newItem.UnitPrice);

                UpdateResult result = await DBMongo.ItemsCollection.UpdateOneAsync(filter,update);

                return result.ModifiedCount;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Select all items from collection
        /// </summary>
        /// <returns></returns>
        public async Task<List<DALItem>> SelectAllAsync()
        {
            return await DBMongo.ItemsCollection.Find(Builders<DALItem>.Filter.Empty).ToListAsync();
        }

        #endregion
    }
}
