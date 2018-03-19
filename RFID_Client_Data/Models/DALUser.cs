using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Data
{
    /// <summary>
    /// Data access layer class for BSON user
    /// </summary>
    public class DALUser
    {
        #region Public Properties

        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("surname")]
        public string Surname { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("password")]
        public SecureString Password { get; set; }

        [BsonElement("dateInserted")]
        public DateTime DateInserted { get; set; }

        [BsonElement("dateUpdated")]
        public DateTime DateUpdated { get; set; }

        #endregion
    }
}
