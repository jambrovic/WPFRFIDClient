namespace RFIDClient.Data
{
    /// <summary>
    /// Configuration of the MongoDB database
    /// </summary>
    public sealed class DBConfiguration
    {

        #region Private Members

        private static readonly string m_DBName = "Receipts";
        private static readonly string m_ReceiptsCollection="Y2017";
        private static readonly string m_ItemsCollection="Items";
        private static readonly string m_UsersCollection="Users";
        private static readonly string m_ConnectionString= "mongodb://localhost:27017";

        #endregion

        #region Constructor

        private DBConfiguration()
        {

        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns the name of the receipts collection
        /// </summary>
        /// <returns></returns>
        public static string GetReceiptsCollection()
        {
            return m_ReceiptsCollection;
        }

        /// <summary>
        /// Returns the name of the items collection
        /// </summary>
        /// <returns></returns>
        public static string GetItemsCollection()
        {
            return m_ItemsCollection;
        }

        /// <summary>
        /// Returns the name of the users collection
        /// </summary>
        /// <returns></returns>
        public static string GetUsersCollection()
        {
            return m_UsersCollection;
        }

        /// <summary>
        /// Returns the connection string parameters
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            return m_ConnectionString;
        }

        /// <summary>
        /// Returns the database name
        /// </summary>
        /// <returns></returns>
        public static string GetDatabaseName()
        {
            return m_DBName;
        }

        #endregion
    }
}
