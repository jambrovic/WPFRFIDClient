using System.Security;

namespace RFIDClient.Desktop
{
    /// <summary>
    /// Service layer class for User entity 
    /// <para/>Insert business logic here
    /// </summary>
    public class User
    {
        #region Public Properties

        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public SecureString Password { get; set; }

        #endregion
    }
}
