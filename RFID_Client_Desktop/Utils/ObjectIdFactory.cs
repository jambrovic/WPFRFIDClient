using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Desktop
{
    public sealed class ObjectIdFactory
    {
        private ObjectIdFactory()
        {

        }

        public static string GetObjectIdString()
        {
            return ObjectId.GenerateNewId().ToString();
        }

        public static ObjectId GetObjectId()
        {
            return ObjectId.GenerateNewId();
        }
    }
}
