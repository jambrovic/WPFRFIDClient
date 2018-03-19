using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Data
{
    public interface ITransaction<R, T> 
        where R : DALReceipt 
        where T : DALReceiptTransaction
    {
        Task<R> Insert(T transaction, ObjectId receiptId);
        Task<T> Select(T transaction, ObjectId receiptId);
        Task<List<T>> SelectAll(T transaction, ObjectId receiptId);
        Task<R> Delete(T transaction, ObjectId receiptId);
        Task<long> Update(T transaction, ObjectId receiptId);
    }
}
