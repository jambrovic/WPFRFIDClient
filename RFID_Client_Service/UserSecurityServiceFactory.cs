namespace RFIDClient.Service
{
    public sealed class UserSecurityServiceFactory
    {
        private static readonly IUserSecurity m_Service = new UserSecurityService();

        private UserSecurityServiceFactory()
        {

        }

        public static IUserSecurity GetSecurity()
        {
            return m_Service;
        }
    }
}
