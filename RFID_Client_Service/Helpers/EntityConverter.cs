using MongoDB.Bson;
using RFIDClient.Data;
using System.Collections.Generic;

namespace RFIDClient.Service
{
    /// <summary>
    /// Converts entities from DAL layer to service layer and vice versa
    /// </summary>
    sealed class EntityConverter
    {
        #region Item Converters

        /// <summary>
        /// Converts <see cref="ItemService"/> to <see cref="DALItem"/>
        /// </summary>
        /// <param name="itemService">The source entity</param>
        /// <returns></returns>
        internal static DALItem GetItem(ItemService itemService)
        {
            return new DALItem
            {
                Barcode = itemService.Barcode,
                Id = ObjectId.Parse(itemService.Id),
                Name = itemService.Name,
                RFIDCode = itemService.RFIDCode,
                SecondaryCode = itemService.SecondaryCode,
                UnitPrice = itemService.UnitPrice
            };
        }

        /// <summary>
        /// Converts <see cref="DALItem"/> to <see cref="ItemService"/>
        /// </summary>
        /// <param name="item">The source entity</param>
        /// <returns></returns>
        internal static ItemService GetItem(DALItem item)
        {
            return new ItemService
            {
                Barcode = item.Barcode,
                Id = item.Id.ToString(),
                Name = item.Name,
                RFIDCode = item.RFIDCode,
                SecondaryCode = item.SecondaryCode,
                UnitPrice = item.UnitPrice
            };
        }

        internal static List<ItemService> GetItems(List<DALItem> items)
        {
            List<ItemService> itemCollection = new List<ItemService>();

            foreach (var i in items)
            {
                itemCollection.Add(GetItem(i));
            }

            return itemCollection;
        }


        internal static List<DALItem> GetItems(List<ItemService> items)
        {
            List<DALItem> itemCollection = new List<DALItem>();

            foreach (var i in items)
            {
                itemCollection.Add(GetItem(i));
            }

            return itemCollection;
        }

        #endregion

        /// <summary>
        /// Converts <see cref="DALUser"/> list to <see cref="UserService"/> list
        /// </summary>
        /// <param name="users">The source entity collection</param>
        /// <returns></returns>
        internal static List<UserService> GetUsers(List<DALUser> users)
        {
            List<UserService> userCollection = new List<UserService>();

            foreach (var i in users)
            {
                userCollection.Add(GetUser(i));
            }

            return userCollection;
        }

        internal static DALUser GetUser(UserService userService)
        {
            return new DALUser
            {
                Email=userService.Email,
                Id=ObjectId.Parse(userService.Id),
                Name=userService.Name,
                Password=userService.Password,
                Surname=userService.Surname,
                Username=userService.Username
            };
        }

        internal static UserService GetUser(DALUser userService)
        {
            return new UserService
            {
                Email = userService.Email,
                Id = userService.Id.ToString(),
                Name = userService.Name,
                Password = userService.Password,
                Surname = userService.Surname,
                Username = userService.Username
            };
        }




        internal static DALReceipt GetReceipt(ReceiptService receipt)
        {
            return new DALReceipt
            {
                DateCreated = receipt.DateCreated,
                DateFinished = receipt.DateFinished,
                Id = ObjectId.Parse(receipt.Id),
                Items = GetTransactions(receipt.Items),
                JIR = receipt.JIR,
                ZKI = receipt.ZKI,
                Total = receipt.Total,
                Payments = GetPayments(receipt.Payments)
            };
        }

        internal static ReceiptService GetReceipt(DALReceipt receipt)
        {
            return new ReceiptService
            {
                DateCreated = receipt.DateCreated,
                DateFinished = receipt.DateFinished,
                Id = receipt.Id.ToString(),
                Items = GetTransactions(receipt.Items),
                JIR = receipt.JIR,
                ZKI = receipt.ZKI,
                Total = receipt.Total,
                Payments = GetPayments(receipt.Payments)
            };
        }

        internal static List<DALReceiptTransaction> GetTransactions(List<TransactionService> transactions)
        {
            List<DALReceiptTransaction> transactionCollection = new List<DALReceiptTransaction>();

            foreach (var t in transactions)
            {
                transactionCollection.Add(new DALReceiptTransaction
                {
                    Barcode = t.Barcode,
                    DiscountPercent = t.DiscountPercent,
                    Id = ObjectId.Parse(t.Id),
                    Name = t.Name,
                    Quantity = t.Quantity,
                    RFIDCode = t.RFIDCode,
                    SecondaryCode = t.SecondaryCode,
                    UnitPrice = t.UnitPrice,
                    Timestamp = t.Timestamp
                });
            }
            return transactionCollection;
        }

        internal static List<TransactionService> GetTransactions(List<DALReceiptTransaction> transactions)
        {
            List<TransactionService> transactionCollection = new List<TransactionService>();

            foreach (var t in transactions)
            {
                transactionCollection.Add(new TransactionService
                {
                    Barcode = t.Barcode,
                    DiscountPercent = t.DiscountPercent,
                    Id = t.Id.ToString(),
                    Name = t.Name,
                    Quantity = t.Quantity,
                    RFIDCode = t.RFIDCode,
                    SecondaryCode = t.SecondaryCode,
                    UnitPrice = t.UnitPrice,
                    Timestamp = t.Timestamp
                });
            }
            return transactionCollection;
        }

        internal static List<DALPayment> GetPayments(List<PaymentService> payments)
        {
            List<DALPayment> paymentsCollection = new List<DALPayment>();

            foreach (var p in payments)
            {
                paymentsCollection.Add(new DALPayment
                {
                    Amount = p.Amount,
                    Code = p.Code,
                    Id = ObjectId.Parse(p.Id),
                    Name = p.Name
                });
            }
            return paymentsCollection;
        }

        internal static List<PaymentService> GetPayments(List<DALPayment> payments)
        {
            List<PaymentService> paymentsCollection = new List<PaymentService>();

            foreach (var p in payments)
            {
                paymentsCollection.Add(new PaymentService
                {
                    Amount = p.Amount,
                    Code = p.Code,
                    Id = p.Id.ToString(),
                    Name = p.Name
                });
            }
            return paymentsCollection;
        }

        internal static List<ReceiptService> GetReceipts(List<DALReceipt> receipts)
        {
            List<ReceiptService> receiptsCollection = new List<ReceiptService>();

            foreach (var r in receipts)
            {
                receiptsCollection.Add(GetReceipt(r));
            }

            return receiptsCollection;
        }

        internal static List<DALReceipt> GetReceipts(List<ReceiptService> receipts)
        {
            List<DALReceipt> receiptsCollection = new List<DALReceipt>();

            foreach (var r in receipts)
            {
                receiptsCollection.Add(GetReceipt(r));
            }

            return receiptsCollection;
        }

        internal static DALReceiptTransaction GetTransaction(TransactionService transaction)
        {
            return new DALReceiptTransaction
            {
                Barcode = transaction.Barcode,
                DiscountPercent = transaction.DiscountPercent,
                Id = ObjectId.Parse(transaction.Id),
                Name = transaction.Name,
                Quantity = transaction.Quantity,
                RFIDCode = transaction.RFIDCode,
                SecondaryCode = transaction.SecondaryCode,
                UnitPrice = transaction.UnitPrice,
                Timestamp = transaction.Timestamp
            };
        }

        internal static TransactionService GetTransaction(DALReceiptTransaction transaction)
        {
            return new TransactionService
            {
                Barcode = transaction.Barcode,
                DiscountPercent = transaction.DiscountPercent,
                Id = transaction.Id.ToString(),
                Name = transaction.Name,
                Quantity = transaction.Quantity,
                RFIDCode = transaction.RFIDCode,
                SecondaryCode = transaction.SecondaryCode,
                UnitPrice = transaction.UnitPrice,
                Timestamp = transaction.Timestamp
            };
        }
    }
}
