namespace RFIDClient.Service
{
    public sealed class ReceiptRepositoryServiceFactory
    {
        private static readonly IRepositoryService<ReceiptService> _service = new ReceiptRepositoryService();

        private ReceiptRepositoryServiceFactory()
        {

        }

        public static IRepositoryService<ReceiptService> GetService()
        {
            return _service;
        }
    }
}
