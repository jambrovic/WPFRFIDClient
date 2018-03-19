namespace RFIDClient.Data
{
    /// <summary>
    /// Factory class for User repository
    /// </summary>
    public sealed class UserFactory
    {
        #region Private Members

        private static readonly IRepository<DALUser> m_Instance = new UserRepository();

        #endregion

        #region Constructor

        private UserFactory() { }

        #endregion

        #region Public Properties

        /// <summary>
        /// Instance of the user repository
        /// </summary>
        public static IRepository<DALUser> Instance => m_Instance;
                
        #endregion
    }
}
