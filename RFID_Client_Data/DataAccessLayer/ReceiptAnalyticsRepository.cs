using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RFIDClient.Data
{
    /// <summary>
    /// Interface for basic analytics functions
    /// </summary>
    public class ReceiptAnalyticsRepository : IAnalytics<DALReceipt>
    {
        #region Private members

        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<DALReceipt> _receiptsCollection;
        private readonly IMongoCollection<DALItem> _itemsCollection;
        private readonly MongoClient _client;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ReceiptAnalyticsRepository()
        {
            _client = new MongoClient(DBConfiguration.GetConnectionString());
            _db = _client.GetDatabase(DBConfiguration.GetDatabaseName());
            _receiptsCollection = _db.GetCollection<DALReceipt>(DBConfiguration.GetReceiptsCollection());
            _itemsCollection = _db.GetCollection<DALItem>(DBConfiguration.GetItemsCollection());
        }
        #endregion

        /// <summary>
        /// <see cref="IEnumerable{DALReceipt}"/> for time period
        /// <para>Beginning and end of time period are included</para>
        /// </summary>
        /// <param name="startDate">Beginning of the time period</param>
        /// <param name="endDate">End of the time period</param>
        /// <returns>Returns <see cref="IEnumerable{DALReceipt}"/></returns>
        public async Task<List<DALReceipt>> GetPeriodAsync(DateTime startDate, DateTime endDate)
        {
            //Filters
            var fromDatefilter = Builders<DALReceipt>.Filter.Where(r => r.DateCreated >= startDate);
            var completeFilter = fromDatefilter & Builders<DALReceipt>.Filter.Where(r=>r.DateCreated <= endDate);

            //DB query
            return await _receiptsCollection.Find(completeFilter).ToListAsync();
        }
    }
}
