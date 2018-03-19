using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using RFIDClient.Data;
using RFIDClient.Service;

namespace RFIDClient.Tests
{
    [TestClass]
    public class ReceiptRowTest
    {
        [TestMethod]
        public async Task TestReceiptSelect()
        {
            DALReceipt receiptForInsert = new DALReceipt
            {
                Id = ObjectId.GenerateNewId(),
                DateCreated = DateTime.Now,
                DateFinished = DateTime.Now.AddSeconds(240),
                JIR = "TestReceiptSelect",
                ZKI = "TestReceiptSelect",
                Payments = new List<DALPayment>
                {
                    new DALPayment
                    {
                        Id=ObjectId.GenerateNewId(),
                        Code="AMEX",
                        Name="American Express",
                        Amount=45.50M
                    },
                    new DALPayment
                    {
                        Id=ObjectId.GenerateNewId(),
                        Code="GOTOVINA",
                        Name="Gotovina",
                        Amount=50.50M
                    }
                },
                Items = new List<DALReceiptTransaction>
                {
                    new DALReceiptTransaction
                    {
                        Id=ObjectId.GenerateNewId(),
                        Name="Item name",
                        Barcode="25252525252525",
                        RFIDCode="787878787878787",
                        Quantity=1,
                        SecondaryCode="ITEM_SECONDARY_CODE",
                        DiscountPercent=0,
                        UnitPrice=96M
                    }
                }
            };

            await ReceiptFactory.GetInstance().InsertAsync(receiptForInsert);
            DALReceipt insertedReceipt = await ReceiptFactory.GetInstance().SelectAsync(receiptForInsert.Id.ToString());
            long deletedReceipts = await ReceiptFactory.GetInstance().DeleteAsync(receiptForInsert.Id.ToString());

            Assert.AreEqual(receiptForInsert.ZKI, insertedReceipt.ZKI);
        }

        [TestMethod]
        public async Task TestReceiptInsert()
        {
            DALReceipt newReceipt = new DALReceipt
            {
                Id = ObjectId.GenerateNewId(),
                DateCreated = DateTime.Now,
                DateFinished = DateTime.Now.AddSeconds(240),
                JIR = "TestReceiptInsert",
                ZKI = "TestReceiptInsert",
                Payments = new List<DALPayment>
                {
                    new DALPayment
                    {
                        Id=ObjectId.GenerateNewId(),
                        Code="AMEX",
                        Name="American Express",
                        Amount=45.50M
                    },
                    new DALPayment
                    {
                        Id=ObjectId.GenerateNewId(),
                        Code="GOTOVINA",
                        Name="Gotovina",
                        Amount=50.50M
                    }
                },
                Items = new List<DALReceiptTransaction>
                {
                    new DALReceiptTransaction
                    {
                        Id=ObjectId.GenerateNewId(),
                        Name="Item name",
                        Barcode="25252525252525",
                        RFIDCode="787878787878787",
                        Quantity=1,
                        SecondaryCode="ITEM_SECONDARY_CODE",
                        DiscountPercent=0,
                        UnitPrice=96M
                    }
                }
            };

            await ReceiptFactory.GetInstance().InsertAsync(newReceipt);
            DALReceipt insertedReceipt = await ReceiptFactory.GetInstance().SelectAsync(newReceipt.Id.ToString());

            Assert.AreEqual(newReceipt.Id, insertedReceipt.Id);
        }

        [TestMethod]
        public async Task TestReceiptServiceInsert()
        {
            ReceiptService newReceipt = new ReceiptService
            {
                Id = ObjectId.GenerateNewId().ToString(),
                DateCreated = DateTime.Now,
                DateFinished = DateTime.Now.AddSeconds(240),
                JIR = "TestReceiptServiceInsert",
                ZKI = "TestReceiptServiceInsert",
                Payments = new List<PaymentService>
                {
                    new PaymentService
                    {
                        Id=ObjectId.GenerateNewId().ToString(),
                        Code="AMEX",
                        Name="American Express",
                        Amount=45.50M
                    },
                    new PaymentService
                    {
                        Id=ObjectId.GenerateNewId().ToString(),
                        Code="GOTOVINA",
                        Name="Gotovina",
                        Amount=50.50M
                    }
                },
                Items = new List<TransactionService>
                {
                    new TransactionService
                    {
                        Id=ObjectId.GenerateNewId().ToString(),
                        Name="Item name",
                        Barcode="25252525252525",
                        RFIDCode="787878787878787",
                        Quantity=1,
                        SecondaryCode="ITEM_SECONDARY_CODE",
                        DiscountPercent=0,
                        UnitPrice=96M
                    }
                }
            };

            await ReceiptRepositoryServiceFactory.GetService().Insert(newReceipt);
            ReceiptService insertedReceipt = await ReceiptRepositoryServiceFactory.GetService().Select(newReceipt.Id.ToString());

            Assert.AreEqual(newReceipt.Id, insertedReceipt.Id);
        }

        [TestMethod]
        public async Task TestReceiptDelete()
        {
            DALReceipt receiptForInsert = new DALReceipt
            {
                Id = ObjectId.GenerateNewId(),
                DateCreated = DateTime.Now,
                DateFinished = DateTime.Now.AddSeconds(240),
                JIR = "TestReceiptDelete",
                ZKI = "TestReceiptDelete",
                Payments = new List<DALPayment>
                {
                    new DALPayment
                    {
                        Id=ObjectId.GenerateNewId(),
                        Code="AMEX",
                        Name="American Express",
                        Amount=45.50M
                    },
                    new DALPayment
                    {
                        Id=ObjectId.GenerateNewId(),
                        Code="GOTOVINA",
                        Name="Gotovina",
                        Amount=50.50M
                    }
                },
                Items = new List<DALReceiptTransaction>
                {
                    new DALReceiptTransaction
                    {
                        Id=ObjectId.GenerateNewId(),
                        Name="Item name",
                        Barcode="25252525252525",
                        RFIDCode="787878787878787",
                        Quantity=1,
                        SecondaryCode="ITEM_SECONDARY_CODE",
                        DiscountPercent=0,
                        UnitPrice=96M
                    }
                }
            };

            await ReceiptFactory.GetInstance().InsertAsync(receiptForInsert);
            long deletedReceipts = await ReceiptFactory.GetInstance().DeleteAsync(receiptForInsert.Id.ToString());

            Assert.AreEqual(1, deletedReceipts);
        }

        [TestMethod]
        public async Task TestReceiptRowSelect()
        {
            DALReceipt receiptForInsert = new DALReceipt
            {
                Id = ObjectId.GenerateNewId(),
                DateCreated = DateTime.Now,
                DateFinished = DateTime.Now.AddSeconds(240),
                JIR = "TestReceiptRowSelect",
                ZKI = "TestReceiptRowSelect",
                Payments = new List<DALPayment>
                {
                    new DALPayment
                    {
                        Id=ObjectId.GenerateNewId(),
                        Code="AMEX",
                        Name="American Express",
                        Amount=45.50M
                    },
                    new DALPayment
                    {
                        Id=ObjectId.GenerateNewId(),
                        Code="GOTOVINA",
                        Name="Gotovina",
                        Amount=50.50M
                    }
                },
                Items = new List<DALReceiptTransaction>
                {
                    new DALReceiptTransaction
                    {
                        Id=ObjectId.GenerateNewId(),
                        Name="Item name",
                        Barcode="25252525252525",
                        RFIDCode="787878787878787",
                        Quantity=1,
                        SecondaryCode="ITEM_SECONDARY_CODE",
                        DiscountPercent=0,
                        UnitPrice=96M
                    }
                }
            };

            await ReceiptFactory.GetInstance().InsertAsync(receiptForInsert);
            DALReceipt receipt = await ReceiptFactory.GetInstance().SelectAsync(receiptForInsert.Id.ToString());

            Assert.AreEqual(receiptForInsert.Items[0].Id, receipt.Items[0].Id);
        }

        [TestMethod]
        public async Task TestReceiptRowDelete()
        {
            DALReceipt receiptForInsert = new DALReceipt
            {
                Id = ObjectId.GenerateNewId(),
                DateCreated = DateTime.Now,
                DateFinished = DateTime.Now.AddSeconds(240),
                JIR = "TestReceiptRowDelete",
                ZKI = "TestReceiptRowDelete",
                Payments = new List<DALPayment>
                {
                    new DALPayment
                    {
                        Id=ObjectId.GenerateNewId(),
                        Code="AMEX",
                        Name="American Express",
                        Amount=45.50M
                    },
                    new DALPayment
                    {
                        Id=ObjectId.GenerateNewId(),
                        Code="GOTOVINA",
                        Name="Gotovina",
                        Amount=50.50M
                    }
                },
                Items = new List<DALReceiptTransaction>
                {
                    new DALReceiptTransaction
                    {
                        Id=ObjectId.GenerateNewId(),
                        Name="Item name",
                        Barcode="25252525252525",
                        RFIDCode="787878787878787",
                        Quantity=1,
                        SecondaryCode="ITEM_SECONDARY_CODE",
                        DiscountPercent=0,
                        UnitPrice=96M
                    },
                    new DALReceiptTransaction
                    {
                        Id=ObjectId.GenerateNewId(),
                        Name="Item name 2",
                        Barcode="25252525252525",
                        RFIDCode="8989898989898989",
                        Quantity=1,
                        SecondaryCode="ITEM_SECONDARY_CODE 2",
                        DiscountPercent=0,
                        UnitPrice=96M
                    }
                }
            };

            await ReceiptFactory.GetInstance().InsertAsync(receiptForInsert);
            DALReceipt updatedReceipt = await TransactionFactory.GetInstance().Delete(receiptForInsert.Items[1], receiptForInsert.Id);

            Assert.AreEqual(1, updatedReceipt.Items.Count);
        }

        [TestMethod]
        public async Task TestReceiptRowInsert()
        {
            DALReceipt receiptForInsert = new DALReceipt
            {
                Id = ObjectId.GenerateNewId(),
                DateCreated = DateTime.Now,
                DateFinished = DateTime.Now.AddSeconds(240),
                JIR = "TestReceiptRowInsert",
                ZKI = "TestReceiptRowInsert",
                Payments = new List<DALPayment>
                {
                    new DALPayment
                    {
                        Id=ObjectId.GenerateNewId(),
                        Code="AMEX",
                        Name="American Express",
                        Amount=45.50M
                    },
                    new DALPayment
                    {
                        Id=ObjectId.GenerateNewId(),
                        Code="GOTOVINA",
                        Name="Gotovina",
                        Amount=50.50M
                    }
                },
                Items=new List<DALReceiptTransaction>()
            };
            DALReceiptTransaction receiptRowForInsert = new DALReceiptTransaction
            {
                Id = ObjectId.GenerateNewId(),
                Name = "Inserted item name",
                Barcode = "25252525252525",
                RFIDCode = "787878787878787",
                Quantity = 1,
                SecondaryCode = "ITEM_SECONDARY_CODE",
                DiscountPercent = 0,
                UnitPrice = 96M
            };

            await ReceiptFactory.GetInstance().InsertAsync(receiptForInsert);
            DALReceipt updatedReceipt = await TransactionFactory.GetInstance().Insert(receiptRowForInsert, receiptForInsert.Id);

            Assert.AreEqual(1, updatedReceipt.Items.Count);
        }
    }
}
