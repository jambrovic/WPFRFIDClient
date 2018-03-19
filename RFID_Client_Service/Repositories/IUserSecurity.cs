using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Service
{
    public interface IUserSecurity
    {
        Task<bool> IsValidUser(string username);

        Task<bool> IsValidPassword(string username, SecureString securePassword);
    }
}
