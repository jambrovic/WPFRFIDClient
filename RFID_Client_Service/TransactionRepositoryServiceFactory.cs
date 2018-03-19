namespace RFIDClient.Service
{
    public sealed class TransactionRepositoryServiceFactory
    {
        private static readonly ITransactionService _service = new TransactionRepositoryService();

        private TransactionRepositoryServiceFactory()
        {

        }

        public static ITransactionService GetService()
        {
            return _service;
        }
    }
}
