using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFIDClient.Data
{
    public class TransactionRepository : ITransaction<DALReceipt, DALReceiptTransaction>
    {
        #region Constructor

        public TransactionRepository() { }

        #endregion

        #region Public Functions

        /// <summary>
        /// Delete receipt transaction by RFID code
        /// </summary>
        /// <param name="transaction">ID of the receipt</param>
        /// <param name="receiptId">Instance of the receipt transaction for delete.</param>
        /// <returns></returns>
        public async Task<DALReceipt> Delete(DALReceiptTransaction transaction, ObjectId receiptId)
        {
            var filter = Builders<DALReceipt>.Filter.Eq("_id", receiptId);
            var update = Builders<DALReceipt>.Update.PullFilter(r => r.Items, i => i.RFIDCode == transaction.RFIDCode);
            var options = new FindOneAndUpdateOptions<DALReceipt>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await DBMongo.ReceiptsCollection.FindOneAndUpdateAsync(filter, update, options);
        }

        public async Task<DALReceipt> Insert(DALReceiptTransaction transaction, ObjectId receiptId)
        {
            var filter = Builders<DALReceipt>.Filter.Eq("_id", receiptId);
            var update = Builders<DALReceipt>.Update.Push(r => r.Items, transaction);
            var options = new FindOneAndUpdateOptions<DALReceipt>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await DBMongo.ReceiptsCollection.FindOneAndUpdateAsync(filter, update, options);
        }

        public async Task<List<DALReceiptTransaction>> SelectAll(DALReceiptTransaction transaction, ObjectId receiptId)
        {
            DALReceipt receipt = await DBMongo.ReceiptsCollection.Find(Builders<DALReceipt>.Filter.Where(r => r.Id.Equals(receiptId))).SingleAsync();
            return receipt.Items;
        }

        public async Task<DALReceiptTransaction> Select(DALReceiptTransaction transaction, ObjectId receiptId)
        {
            DALReceipt receipt = await DBMongo.ReceiptsCollection.Find(Builders<DALReceipt>.Filter.Where(r => r.Id.Equals(receiptId))).SingleAsync();
            return receipt.Items.Where(r => r.RFIDCode == transaction.RFIDCode).FirstOrDefault();
        }

        public Task<long> Update(DALReceiptTransaction transaction, ObjectId receiptId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
