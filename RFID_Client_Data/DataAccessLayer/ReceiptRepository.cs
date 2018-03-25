using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RFIDClient.Data
{
    class ReceiptRepository : IRepository<DALReceipt>
    {
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<DALReceipt> _receiptsCollection;
        private readonly IMongoCollection<DALItem> _itemsCollection;
        private readonly MongoClient _client;

        public ReceiptRepository()
        {
            _client = new MongoClient(DBConfiguration.GetConnectionString());
            _db = _client.GetDatabase(DBConfiguration.GetDatabaseName());
            _receiptsCollection = _db.GetCollection<DALReceipt>(DBConfiguration.GetReceiptsCollection());
            _itemsCollection = _db.GetCollection<DALItem>(DBConfiguration.GetItemsCollection());
        }
        public async Task InsertAsync(DALReceipt receipt) => await _receiptsCollection.InsertOneAsync(receipt);

        public async Task<DALReceipt> SelectAsync(string id) => await _receiptsCollection.Find(Builders<DALReceipt>.Filter.Where(r => r.Id.Equals(ObjectId.Parse(id)))).SingleAsync();
        
        public async Task<long> DeleteAsync(string id)
        {
            DeleteResult result = await _receiptsCollection.DeleteOneAsync(Builders<DALReceipt>.Filter.Where(r => r.Id.Equals(ObjectId.Parse(id))));
            return result.DeletedCount;
        }

        //public async Task<Receipt> InsertTransaction(Transaction receiptRow, ObjectId receiptId)
        //{
        //    var filter = Builders<Receipt>.Filter.Eq("_id", receiptId);
        //    var update = Builders<Receipt>.Update.Push(r => r.Items, receiptRow);
        //    var options = new FindOneAndUpdateOptions<Receipt>
        //    {
        //        ReturnDocument = ReturnDocument.After
        //    };

        //    return await _receiptsCollection.FindOneAndUpdateAsync(filter, update, options);
        //}

        //public async Task<Receipt> DeleteTransaction(Transaction receiptRow, ObjectId receiptId)
        //{
        //    var filter = Builders<Receipt>.Filter.Eq("_id", receiptId);
        //    var update = Builders<Receipt>.Update.PullFilter(r => r.Items, i => i.RFIDCode == receiptRow.RFIDCode);
        //    var options = new FindOneAndUpdateOptions<Receipt>
        //    {
        //        ReturnDocument = ReturnDocument.After
        //    };

        //    return await _receiptsCollection.FindOneAndUpdateAsync(filter, update, options);
        //}

        //public Task<long> UpdateTransaction(Transaction receiptRow, ObjectId receiptId)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<Transaction> SelectTransaction(Transaction receiptRow, ObjectId receiptId)
        //{
        //    Receipt receipt = await _receiptsCollection.Find(Builders<Receipt>.Filter.Where(r => r.Id.Equals(receiptId))).SingleAsync();
        //    return receipt.Items.Where(r => r.RFIDCode == receiptRow.RFIDCode).FirstOrDefault();
        //}

        //public Task<IEnumerable<Transaction>> SelectAllTransactions(Transaction receiptRow, ObjectId receiptId)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<long> UpdateAsync(DALReceipt entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DALReceipt>> SelectAllAsync()
        {
            return await _receiptsCollection.Find(Builders<DALReceipt>.Filter.Empty).ToListAsync();
        }
    }
}
