using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Desktop.Core
{
    public interface IUser
    {
        /// <summary>
        /// Clear the UI input controls
        /// </summary>
        void ResetForm();

        /// <summary>
        /// The Name of the user
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// The Surname of the user
        /// </summary>
        string LastName { get; }

        /// <summary>
        /// The Email of the user
        /// </summary>
        string Email { get; }

        /// <summary>
        /// The username of the user
        /// </summary>
        string Username { get; }

        /// <summary>
        /// The password of the user
        /// </summary>
        SecureString Password { get; }

        /// <summary>
        /// The password of the user (retiyped)
        /// </summary>
        SecureString Password2 { get; }

    }
}
