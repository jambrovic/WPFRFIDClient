namespace RFIDClient.Data
{
    /// <summary>
    /// Factory class for Receipt repository
    /// </summary>
    public sealed class ReceiptFactory
    {
        #region Private Members

        private static readonly IRepository<DALReceipt> m_Instance = new ReceiptRepository();

        #endregion

        #region Constructor

        private ReceiptFactory() { }

        #endregion

        #region Public Functions

        /// <summary>
        /// Returns instance of Receipt repository
        /// </summary>
        /// <returns></returns>
        public static IRepository<DALReceipt> GetInstance()
        {
            return m_Instance;
        }
        
        #endregion
    }
}
