using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Data
{
    /// <summary>
    /// Data access layer class for BSON item
    /// </summary>
    public class DALItem
    {
        #region Public Properties

        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId Id { get; set; }

        [BsonElement("barcode")]
        public string Barcode { get; set; }

        [BsonElement("rfidCode")]
        public string RFIDCode { get; set; }

        [BsonElement("secondaryCode")]
        public string SecondaryCode { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("unitPrice")]
        public decimal UnitPrice { get; set; }

        #endregion
    }
}
