using MongoDB.Driver;

namespace RFIDClient.Data
{
    /// <summary>
    /// Base static class for getting Mongo entites (client, database, collections)
    /// </summary>
    public sealed class DBMongo
    {

        #region Constructor

        private DBMongo() { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the instance of the MongoClient class
        /// </summary>
        public static MongoClient Client => new MongoClient(DBConfiguration.GetConnectionString());

        /// <summary>
        /// Gets the instance of the Mongo database class
        /// </summary>
        public static IMongoDatabase Database => Client.GetDatabase(DBConfiguration.GetDatabaseName());

        /// <summary>
        /// Gets the instance of the Receipts collection
        /// </summary>
        public static IMongoCollection<DALReceipt> ReceiptsCollection => Database.GetCollection<DALReceipt>(DBConfiguration.GetReceiptsCollection());

        /// <summary>
        /// Gets the instance of the Items collection
        /// </summary>
        public static IMongoCollection<DALItem> ItemsCollection => Database.GetCollection<DALItem>(DBConfiguration.GetItemsCollection());

        /// <summary>
        /// Gets the instance of the Users collection
        /// </summary>
        public static IMongoCollection<DALUser> UsersCollection => Database.GetCollection<DALUser>(DBConfiguration.GetUsersCollection());

        #endregion
    }
}
