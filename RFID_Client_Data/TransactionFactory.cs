namespace RFIDClient.Data
{
    public sealed class TransactionFactory
    {
        private static readonly ITransaction<DALReceipt,DALReceiptTransaction> _instance = new TransactionRepository();

        private TransactionFactory()
        {

        }

        public static ITransaction<DALReceipt, DALReceiptTransaction> GetInstance()
        {
            return _instance;
        }
    }
}
