using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Service
{
    public class UserSecurityService : IUserSecurity
    {
        public async Task<bool> IsValidPassword(string username, SecureString securePassword)
        {
            return true;
        }

        public async Task<bool> IsValidUser(string username)
        {
            return true;
        }
    }
}
