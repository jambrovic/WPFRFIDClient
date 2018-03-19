using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Desktop.Core
{
    /// <summary>
    /// A page of the application
    /// </summary>
    public enum ApplicationPage
    {
        /// <summary>
        /// Initial login page
        /// </summary>
        Login = 0,
        Receipt = 1,
        Payment = 2,
        NewItem = 3,
        NewUser = 4,
        UpdateUser = 5,
        EndOfTheDay = 6,
        AllItems = 7,
        None = 8,
        AppOptions = 9,
        AppAbout = 10,
    }
}
