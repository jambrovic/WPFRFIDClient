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

        /// <summary>
        /// Default constructor (private)
        /// </summary>
        public ItemRepository() { }

        #endregion

        #region Public Functions

        /// <summary>
        /// Selects a single item (async)
        /// </summary>
        /// <param name="rfid">RFID code of the item</param>
        /// <returns>Returns <see cref="Task{DALItem}"/></returns>
        public async Task<DALItem> SelectAsync(string rfid)
        {
            //Filter builder
            var builder = Builders<DALItem>.Filter;

            //Filter definition
            var filter = builder.Eq("rfidCode", rfid);

            //DB query
            return await DBMongo.ItemsCollection.Find(filter).SingleAsync();
        }

        /// <summary>
        /// Inserts a single item (async)
        /// </summary>
        /// <param name="item">The instance of the item object for insert</param>
        /// <returns>Returns <see cref="Task"/></returns>
        public async Task InsertAsync(DALItem item)
        {
            #region Delete item if exists

            //Filter builder
            var builder = Builders<DALItem>.Filter;

            //Filter define
            var filter = builder.Eq("rfidCode", item.RFIDCode);

            //DB Delete document if exists
            DeleteResult deleteResult = await DBMongo.ItemsCollection.DeleteManyAsync(filter);

            #endregion

            //DB Insert document
            await DBMongo.ItemsCollection.InsertOneAsync(item);
        }

        /// <summary>
        /// Delete a single item
        /// </summary>
        /// <param name="rfid">RFID code of the item for delete</param>
        /// <returns>Count of deleted documents</returns>
        public async Task<long> DeleteAsync(string rfid)
        {
            //Filter builder
            var builder = Builders<DALItem>.Filter;

            //Filter define
            var filter = builder.Eq("rfidCode", rfid);

            //DB delete
            DeleteResult result = await DBMongo.ItemsCollection.DeleteManyAsync(filter);

            return result.DeletedCount;
        }

        /// <summary>
        /// Updates a single item by RFID code
        /// </summary>
        /// <param name="newItem">The instance of the updated item</param>
        /// <returns>Returns a number of the updated items</returns>
        public async Task<long> UpdateAsync(DALItem newItem)
        {
            //Select item that we want to update by RFID code
            DALItem itemForUpdate = await SelectAsync(newItem.RFIDCode);

            //Filter builder
            var builder = Builders<DALItem>.Filter;

            //Filter definition
            var filter = builder.Eq("_id", itemForUpdate.Id);

            //Update statement
            var update = Builders<DALItem>.Update
                .Set("barcode", newItem.Barcode)
                .Set("name", newItem.Name)
                .Set("secondaryCode", newItem.SecondaryCode)
                .Set("unitPrice", newItem.UnitPrice);

            //DB update execute
            UpdateResult result = await DBMongo.ItemsCollection.UpdateOneAsync(filter, update);

            return result.ModifiedCount;
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
