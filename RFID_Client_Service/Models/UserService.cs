using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient
{
    /// <summary>
    /// Service layer class for User entity 
    /// <para/>Insert business logic here
    /// </summary>
    public class UserService
    {
        #region Public Properties

        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public SecureString Password { get; set; }

        public DateTime DateInserted { get; set; }
        
        public DateTime DateUpdated { get; set; }

        #endregion
    }
}
