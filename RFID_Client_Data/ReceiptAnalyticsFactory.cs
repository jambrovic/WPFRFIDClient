namespace RFIDClient.Data
{
    /// <summary>
    /// Receipty analytics factory class
    /// </summary>
    public sealed class ReceiptAnalyticsFactory
    {
        #region Private Members

        /// <summary>
        /// <see cref="IAnalytics{DALReceipt}"/> instance
        /// </summary>
        private static IAnalytics<DALReceipt> m_Instance = new ReceiptAnalyticsRepository();
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        private ReceiptAnalyticsFactory() { }

        #endregion

        #region Public Properties

        /// <summary>
        /// <see cref="IAnalytics{DALReceipt}"/> instance
        /// </summary>
        public static IAnalytics<DALReceipt> Instance => m_Instance; 
        #endregion
    }
}
