namespace RFIDClient.Service
{
    public sealed class ItemRepositoryServiceFactory
    {
        private static readonly IRepositoryService<ItemService> _service = new ItemRepositoryService();

        private ItemRepositoryServiceFactory()
        {

        }

        public static IRepositoryService<ItemService> GetService()
        {
            return _service;
        }
    }
}
