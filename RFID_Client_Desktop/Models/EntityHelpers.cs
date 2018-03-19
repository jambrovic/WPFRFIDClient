using MongoDB.Bson;
using RFIDClient.Desktop.Core;
using RFIDClient.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RFIDClient.Desktop
{
    public sealed class EntityHelpers
    {
        public static ItemService GetItem(Item itemViewModel)
        {
            if (itemViewModel != null)
            {
                return new ItemService
                {
                    Barcode = itemViewModel.Barcode,
                    Id = itemViewModel.Id==null ? ObjectId.GenerateNewId().ToString() : itemViewModel.Id,
                    Name = itemViewModel.Name,
                    RFIDCode = itemViewModel.RFIDCode,
                    SecondaryCode = itemViewModel.SecondaryCode,
                    UnitPrice = itemViewModel.UnitPrice
                };
            }
            return null;
        }

        public static ItemService GetItem(ItemViewModel itemViewModel)
        {
            if (itemViewModel != null)
            {
                return new ItemService
                {
                    Barcode = itemViewModel.Barcode,
                    Id = itemViewModel.Id == null ? ObjectId.GenerateNewId().ToString() : itemViewModel.Id,
                    Name = itemViewModel.Name,
                    RFIDCode = itemViewModel.RFIDCode,
                    SecondaryCode = itemViewModel.SecondaryCode,
                    UnitPrice = itemViewModel.UnitPrice
                };
            }
            return null;
        }

        public static Item GetItem(ItemService item)
        {
            if (item != null)
            {
                return new Item
                {
                    Barcode = item.Barcode,
                    Id = item.Id,
                    Name = item.Name,
                    RFIDCode = item.RFIDCode,
                    SecondaryCode = item.SecondaryCode,
                    UnitPrice = item.UnitPrice
                };
            }
            return null;
        }

        public static ItemViewModel GetItemVM(ItemService item)
        {
            if (item != null)
            {
                return new ItemViewModel
                {
                    Barcode = item.Barcode,
                    Id = item.Id,
                    Name = item.Name,
                    RFIDCode = item.RFIDCode,
                    SecondaryCode = item.SecondaryCode,
                    UnitPrice = item.UnitPrice
                };
            }
            return null;
        }

        public static List<ItemService> GetItems(List<Item> items)
        {
            List<ItemService> itemCollection = new List<ItemService>();
            if (items != null)
            {
                foreach (var i in items)
                {
                    itemCollection.Add(GetItem(i));
                }
            }
            return itemCollection;
        }

        public static List<Item> GetItems(List<ItemService> items)
        {
            List<Item> itemCollection = new List<Item>();
            if (items != null)
            {

                foreach (var i in items)
                {
                    itemCollection.Add(GetItem(i));
                }
            }
            return itemCollection;
        }

        public static ObservableCollection<ItemViewModel> GetObservableItems(List<ItemService> items)
        {
            ObservableCollection<ItemViewModel> itemCollection = new ObservableCollection<ItemViewModel>();

            if (items != null)
            {

                foreach (var i in items)
                {
                    itemCollection.Add(GetItemVM(i));
                }
            }
            return itemCollection;
        }

        public static ReceiptService GetReceipt(Receipt receipt)
        {
            if (receipt != null)
            {
                return new ReceiptService
                {
                    DateCreated = receipt.DateCreated,
                    DateFinished = receipt.DateFinished,
                    Id = receipt.Id,
                    Items = GetTransactions(receipt.Items),
                    JIR = receipt.JIR,
                    ZKI = receipt.ZKI,
                    Total = receipt.Total,
                    Payments = GetPayments(receipt.Payments)
                };
            }
            return null;
        }

        public static ReceiptService GetReceipt(ReceiptViewModel receipt)
        {
            if (receipt != null)
            {
                return new ReceiptService
                {
                    DateCreated = receipt.DateCreated,
                    DateFinished = receipt.DateFinished,
                    Id = receipt.Id,
                    Items = GetTransactions(receipt.Transactions),
                    JIR = receipt.JIR,
                    ZKI = receipt.ZKI,
                    Total = receipt.Total,
                    Payments = GetPayments(receipt.Payments)
                };
            }
            return null;
        }

        public static Receipt GetReceipt(ReceiptService receipt)
        {
            if (receipt != null)
            {
                return new Receipt
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
            return null;
        }

        public static List<TransactionService> GetTransactions(List<ReceiptTransaction> transactions)
        {
            List<TransactionService> transactionCollection = new List<TransactionService>();
            if (transactions != null)
            {
                foreach (var t in transactions)
                {
                    transactionCollection.Add(new TransactionService
                    {
                        Barcode = t.Barcode,
                        DiscountPercent = t.DiscountPercent,
                        Id = t.Id,
                        Name = t.Name,
                        Quantity = t.Quantity,
                        RFIDCode = t.RFIDCode,
                        SecondaryCode = t.SecondaryCode,
                        UnitPrice = t.UnitPrice,
                        Timestamp = t.Timestamp
                    });
                }
            }
            return transactionCollection;
        }

        public static List<TransactionService> GetTransactions(ObservableCollection<TransactionViewModel> transactions)
        {
            List<TransactionService> transactionCollection = new List<TransactionService>();
            if (transactions != null)
            {
                foreach (var t in transactions)
                {
                    transactionCollection.Add(new TransactionService
                    {
                        Barcode = t.Barcode,
                        DiscountPercent = t.DiscountPercent,
                        Id = t.Id,
                        Name = t.Name,
                        Quantity = t.Quantity,
                        RFIDCode = t.RFIDCode,
                        SecondaryCode = t.SecondaryCode,
                        UnitPrice = t.UnitPrice,
                        Timestamp = t.Timestamp
                    });
                }
            }
            return transactionCollection;
        }

        public static List<ReceiptTransaction> GetTransactions(List<TransactionService> transactions)
        {
            List<ReceiptTransaction> transactionCollection = new List<ReceiptTransaction>();
            if (transactions != null)
            {
                foreach (var t in transactions)
                {
                    transactionCollection.Add(new ReceiptTransaction
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
            }
            return transactionCollection;
        }

        public static List<PaymentService> GetPayments(List<ReceiptPayment> payments)
        {
            List<PaymentService> paymentsCollection = new List<PaymentService>();
            if (payments != null)
            {
                foreach (var p in payments)
                {
                    paymentsCollection.Add(new PaymentService
                    {
                        Amount = p.Amount,
                        Code = p.Code,
                        Id = p.Id,
                        Name = p.Name
                    });
                }
            }
            return paymentsCollection;
        }

        public static List<PaymentService> GetPayments(ObservableCollection<PaymentViewModel> payments)
        {
            List<PaymentService> paymentsCollection = new List<PaymentService>();
            if (payments != null)
            {
                foreach (var p in payments)
                {
                    paymentsCollection.Add(new PaymentService
                    {
                        Amount = p.Amount,
                        Code = p.Code,
                        Id = p.Id,
                        Name = p.Name
                    });
                }
            }
            return paymentsCollection;
        }

        public static List<ReceiptPayment> GetPayments(List<PaymentService> payments)
        {
            List<ReceiptPayment> paymentsCollection = new List<ReceiptPayment>();
            if (payments != null)
            {
                foreach (var p in payments)
                {
                    paymentsCollection.Add(new ReceiptPayment
                    {
                        Amount = p.Amount,
                        Code = p.Code,
                        Id = p.Id.ToString(),
                        Name = p.Name
                    });
                }
            }
            return paymentsCollection;
        }
    }
}
