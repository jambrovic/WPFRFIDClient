using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using RFIDClient.Data;
using RFIDClient.Service;

namespace RFIDClient.Tests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public async Task TestItemInsert()
        {
            DALItem item = new DALItem
            {
                Id=ObjectId.GenerateNewId(),
                Barcode = "1234567891234",
                Name = "UnitTestItem #1",
                RFIDCode = "123456789",
                SecondaryCode = "UnitTest_1",
                UnitPrice = 20.22M
            };

            IRepository<DALItem> itemsRepo = ItemFactory.GetInstance();
            await itemsRepo.InsertAsync(item);

            DALItem insertedItem = await itemsRepo.SelectAsync(item.RFIDCode);

            Assert.AreEqual(item.RFIDCode, insertedItem.RFIDCode);
        }

        [TestMethod]
        public async Task TestItemServiceInsert()
        {
            ItemService item = new ItemService
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Barcode = "1234567891234",
                Name = "UnitTestItemService #1",
                RFIDCode = "123456789",
                SecondaryCode = "UnitTest_1",
                UnitPrice = 20.22M
            };

            IRepositoryService<ItemService> itemsRepo = ItemRepositoryServiceFactory.GetService();
            await itemsRepo.Insert(item);

            ItemService insertedItem = await itemsRepo.Select(item.RFIDCode);

            Assert.AreEqual(item.RFIDCode, insertedItem.RFIDCode);
        }

        [TestMethod]
        public async Task TestItemSelect()
        {
            DALItem itemForInsert = new DALItem
            {
                Id = ObjectId.GenerateNewId(),
                Barcode = "1234567891234",
                Name = "TestItemSelect #1",
                RFIDCode = "TestItemSelect",
                SecondaryCode = "TestItemSelect",
                UnitPrice = 20.22M
            };
            
            await ItemFactory.GetInstance().InsertAsync(itemForInsert);
            DALItem selectedItem = await ItemFactory.GetInstance().SelectAsync("TestItemSelect");
            await ItemFactory.GetInstance().DeleteAsync(selectedItem.RFIDCode);

            Assert.AreEqual(selectedItem.SecondaryCode, itemForInsert.SecondaryCode);
        }

        [TestMethod]
        public async Task TestItemUpdate()
        {
            IRepository<DALItem> itemsRepo = ItemFactory.GetInstance();
            await itemsRepo.DeleteAsync("someRfidCode");

            DALItem itemForInsert = new DALItem
            {
                
                Barcode = "00000000000000",
                Name = "TestItemUpdate inserted item #1",
                RFIDCode = "TestItemUpdate",
                SecondaryCode = "TestItemUpdate inserted",
                UnitPrice = 33.33M
            };

            DALItem itemForUpdate = new DALItem
            {
                Barcode = "66666666666666666",
                Name = "TestItemUpdate updated item #1",
                RFIDCode = "TestItemUpdate",
                SecondaryCode = "TestItemUpdate updated",
                UnitPrice = 11.11M
            };

            await itemsRepo.InsertAsync(itemForInsert);

            long result = await itemsRepo.UpdateAsync(itemForUpdate);

            DALItem updatedItem = await itemsRepo.SelectAsync(itemForInsert.RFIDCode);

            Assert.AreEqual(itemForUpdate.Name, updatedItem.Name);
        }
        [TestMethod]
        public async Task TestItemDelete()
        {
            DALItem itemForDelete = new DALItem
            {

                Barcode = "00000000000000",
                Name = "TestItemDelete #1",
                RFIDCode = "TestItemDelete",
                SecondaryCode = "TestItemDelete",
                UnitPrice = 33.33M
            };

            await ItemFactory.GetInstance().InsertAsync(itemForDelete);
            long deletedItemsCount = await ItemFactory.GetInstance().DeleteAsync("TestItemDelete");

            Assert.AreNotEqual(0, deletedItemsCount);
        }

    }
}
