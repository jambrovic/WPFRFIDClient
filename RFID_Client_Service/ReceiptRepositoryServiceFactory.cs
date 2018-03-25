namespace RFIDClient.Service
{
    public sealed class ReceiptRepositoryServiceFactory
    {
        private static readonly IRepositoryService<ReceiptService> m_Instance = new ReceiptRepositoryService();

        private static readonly IAnalyticsService<ReceiptService> m_Analytics = new ReceiptRepositoryService();

        private ReceiptRepositoryServiceFactory() { }

        public static IRepositoryService<ReceiptService> Instance => m_Instance;
        public static IAnalyticsService<ReceiptService> Analytics => m_Analytics;
    }
}
