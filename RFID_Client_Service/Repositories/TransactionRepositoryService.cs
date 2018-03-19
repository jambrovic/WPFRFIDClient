using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using RFIDClient.Data;

namespace RFIDClient.Service
{
    class TransactionRepositoryService : ITransactionService
    {
        public async Task<ReceiptService> Delete(TransactionService transaction, ObjectId receiptId)
        {
            return EntityConverter.GetReceipt(await TransactionFactory.GetInstance().Delete(EntityConverter.GetTransaction(transaction), receiptId));
        }

        public async Task<ReceiptService> Insert(TransactionService transaction, ObjectId receiptId)
        {
            return EntityConverter.GetReceipt(await TransactionFactory.GetInstance().Insert(EntityConverter.GetTransaction(transaction), receiptId));
        }

        public async Task<TransactionService> Select(TransactionService transaction, ObjectId receiptId)
        {
            return EntityConverter.GetTransaction(await TransactionFactory.GetInstance().Select(EntityConverter.GetTransaction(transaction), receiptId));
        }

        public async Task<List<TransactionService>> SelectAll(TransactionService transaction, ObjectId receiptId)
        {
            return EntityConverter.GetTransactions(await TransactionFactory.GetInstance().SelectAll(EntityConverter.GetTransaction(transaction), receiptId));
        }

        public async Task<long> Update(TransactionService transaction, ObjectId receiptId)
        {
            return await TransactionFactory.GetInstance().Update(EntityConverter.GetTransaction(transaction), receiptId);
        }
    }
}
