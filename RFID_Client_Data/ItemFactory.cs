namespace RFIDClient.Data
{
    public sealed class ItemFactory
    {
        private static readonly IRepository<DALItem> _instance = new ItemRepository();
        private ItemFactory()
        {

        }

        public static IRepository<DALItem> GetInstance()
        {
            return _instance;
        }
    }
}
