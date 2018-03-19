using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Data
{
    /// <summary>
    /// User autentification class for validating username and password
    /// </summary>
    class UserSecurity : ISecurity
    {
        public async Task<bool> IsValidPassword(string username, SecureString securePassword)
        {
            var user = await UserFactory.Instance.SelectAsync(username);
            if (user != null && user.Password.Equals(securePassword))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsValidUser(string username)
        {
            var user = await UserFactory.Instance.SelectAsync(username);
            if (user!=null)
            {
                return true;
            }
            
            return false;
        }
    }
}
