using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RFIDClient.Data
{
    /// <summary>
    /// Class for CRUD operations on User collection
    /// </summary>
    class UserRepository : IRepository<DALUser>
    {
        #region Constructor

        public UserRepository() { }

        #endregion

        #region Public Functions

        /// <summary>
        /// Select a single user
        /// </summary>
        /// <param name="username">RFID string of the item</param>
        /// <returns></returns>
        public async Task<DALUser> SelectAsync(string username)
        {
            try
            {
                var builder = Builders<DALUser>.Filter;
                var filter = builder.Eq("username", username);
                return await DBMongo.UsersCollection.Find(filter).SingleAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Insert a single item
        /// </summary>
        /// <param name="user">The instance of the item object for insert</param>
        /// <returns></returns>
        public async Task InsertAsync(DALUser user)
        {
            try
            {
                //#region Delete item if exists

                //var builder = Builders<DALItem>.Filter;
                //var filter = builder.Eq("rfidCode", user.RFIDCode);
                //DeleteResult deleteResult = await DBMongo.ItemsCollection.DeleteManyAsync(filter);

                //#endregion

                await DBMongo.UsersCollection.InsertOneAsync(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete a single item
        /// </summary>
        /// <param name="username">RFID code of the item for delete</param>
        /// <returns>Count of deleted documents</returns>
        public async Task<long> DeleteAsync(string username)
        {
            try
            {
                var builder = Builders<DALUser>.Filter;
                var filter = builder.Eq("username", username);
                DeleteResult result = await DBMongo.UsersCollection.DeleteManyAsync(filter);
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
        /// <param name="newUser">The instance of the updated item</param>
        /// <returns>Returns a number of the updated items</returns>
        public async Task<long> UpdateAsync(DALUser newUser)
        {
            try
            {
                DALUser userForUpdate = await SelectAsync(newUser.Username);

                var builder = Builders<DALUser>.Filter;
                var filter = builder.Eq("username", userForUpdate.Username);
                var update = Builders<DALUser>.Update
                    .Set("name", newUser.Name)
                    .Set("surname", newUser.Surname)
                    .Set("email", newUser.Email)
                    .Set("password", newUser.Password);

                UpdateResult result = await DBMongo.UsersCollection.UpdateOneAsync(filter,update);

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
        public async Task<List<DALUser>> SelectAllAsync()
        {
            return await DBMongo.UsersCollection.Find(Builders<DALUser>.Filter.Empty).ToListAsync();
        }

        #endregion
    }
}
