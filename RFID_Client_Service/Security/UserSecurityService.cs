using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace RFIDClient.Service
{
    public class UserSecurityService : IUserSecurity
    {
        public async Task<bool> IsValidPassword(string username, SecureString securePassword)
        {
            var users = await UserRepositoryServiceFactory.Service.SelectAll();

            foreach (var user in users)
            {
                var hash1 = user.Password.GetHashCode();
                var hash2 = securePassword.GetHashCode();

                if (user.Username != null && 
                    user.Username.Equals(username) && 
                    securePassword!= null && 
                    SecureStringHelpers.HashSHA1(securePassword.ToString()).Equals(user.Password))
                {
                    return true;
                }

            }
            return false;
        }

        public async Task<bool> IsValidUser(string username)
        {
            return true;
        }

        Boolean SecureStringEqual(SecureString secureString1, SecureString secureString2)
        {
            if (secureString1 == null)
            {
                throw new ArgumentNullException("s1");
            }
            if (secureString2 == null)
            {
                throw new ArgumentNullException("s2");
            }

            if (secureString1.Length != secureString2.Length)
            {
                return false;
            }

            IntPtr ss_bstr1_ptr = IntPtr.Zero;
            IntPtr ss_bstr2_ptr = IntPtr.Zero;

            try
            {
                ss_bstr1_ptr = Marshal.SecureStringToBSTR(secureString1);
                ss_bstr2_ptr = Marshal.SecureStringToBSTR(secureString2);

                String str1 = Marshal.PtrToStringBSTR(ss_bstr1_ptr);
                String str2 = Marshal.PtrToStringBSTR(ss_bstr2_ptr);

                return str1.Equals(str2);
            }
            finally
            {
                if (ss_bstr1_ptr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(ss_bstr1_ptr);
                }

                if (ss_bstr2_ptr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(ss_bstr2_ptr);
                }
            }
        }
    }
}
