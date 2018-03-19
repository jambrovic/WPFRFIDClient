namespace RFIDClient.Desktop
{
    /// <summary>
    /// Displays currency name 
    /// </summary>
    public static class Currency
    {
        #region Private Members

        private static CurrencyName m_Currency;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="currencyName"></param>
        public static void SetCurrency(CurrencyName currencyName)
        {
            m_Currency = currencyName;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Current currency name
        /// </summary>
        public static string Name
        {
            get
            {
                switch (m_Currency)
                {
                    case CurrencyName.KN:
                        return "Kn";
                        
                    case CurrencyName.EUR:
                        return "EUR";
                        
                    case CurrencyName.USD:
                        return "$";
                        
                    default:
                        return "Kn";
                }
            }

        }
        #endregion
    }
}
