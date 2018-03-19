using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace RFIDClient.Data
{
    public class DALReceiptTransaction
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId Id { get; set; }

        [BsonElement("barcode")]
        [BsonRequired]
        public string Barcode { get; set; }

        [BsonElement("rfidCode")]
        [BsonRequired]
        public string RFIDCode { get; set; }

        [BsonElement("secondaryCode")]
        [BsonRequired]
        public string SecondaryCode { get; set; }

        [BsonElement("name")]
        [BsonRequired]
        public string Name { get; set; }

        [BsonElement("quantity")]
        [BsonRequired]
        public decimal Quantity { get; set; }

        [BsonElement("unitPrice")]
        [BsonRequired]
        public decimal UnitPrice { get; set; }

        [BsonElement("discountPercent")]
        [BsonRequired]
        public decimal DiscountPercent { get; set; }

        [BsonElement("total")]
        [BsonRequired]
        public decimal Total
        {
            get
            {
                return Math.Round(Quantity * UnitPrice * ((100 - DiscountPercent) / 100), 2);
            }
        }
        [BsonElement("timestamp")]
        [BsonRequired]
        public DateTime Timestamp { get; set; }
    }
}
