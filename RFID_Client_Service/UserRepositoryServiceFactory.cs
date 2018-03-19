namespace RFIDClient.Service
{
    public sealed class UserRepositoryServiceFactory
    {
        private static readonly IRepositoryService<UserService> m_Service = new UserRepositoryService();

        private UserRepositoryServiceFactory()
        {

        }

        public static IRepositoryService<UserService> Service => m_Service;

        public static IRepositoryService<UserService> GetService()
        {
            return m_Service;
        }
    }
}
