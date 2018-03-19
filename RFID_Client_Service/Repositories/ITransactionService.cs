using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Service
{
    public interface ITransactionService
    {
        Task<ReceiptService> Insert(TransactionService transaction, ObjectId receiptId);
        Task<TransactionService> Select(TransactionService transaction, ObjectId receiptId);
        Task<List<TransactionService>> SelectAll(TransactionService transaction, ObjectId receiptId);
        Task<ReceiptService> Delete(TransactionService transaction, ObjectId receiptId);
        Task<long> Update(TransactionService transaction, ObjectId receiptId);
    }
}
