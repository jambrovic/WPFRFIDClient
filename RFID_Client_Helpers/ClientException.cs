using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Helpers
{
    public sealed class ClientException
    {
        public static string GetFormatedException(Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("Message: ");
            str.Append(ex.Message);
            str.Append(Environment.NewLine);

            str.Append("Stacktrace: ");
            str.Append(ex.StackTrace);
            str.Append(Environment.NewLine);

            str.Append("Data: ");
            str.Append(ex.Data);
            str.Append(Environment.NewLine);

            str.Append("Source: ");
            str.Append(ex.Source);
            str.Append(Environment.NewLine);

            str.Append("Inner Exception: ");
            str.Append(ex.InnerException);
            str.Append(Environment.NewLine);

            return str.ToString();
        }
    }
}
