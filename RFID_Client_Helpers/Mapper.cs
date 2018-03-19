using MongoDB.Bson;
using RFIDClient.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Helpers
{
    public sealed class Mapper
    {
        public static BsonDocument GetBsonDoc(DALReceipt receipt)
        {
            var doc = new BsonDocument {
                {"dateCreated",receipt.DateCreated },
                {"total",receipt.Total },
                {"zki", receipt.ZKI },
                {"jir", receipt.JIR },
                {"dateFinished",receipt.DateFinished },
            };

            

            return doc;
        }
    }
}
