using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.BackOffice.Service
{
    public static class BackOfficeHelper
    {
        public static string GetMD5Hash(string original)
        {
            using (MD5 md5hash = MD5.Create())
            {
                byte[] computedHash = md5hash.ComputeHash(Encoding.UTF8.GetBytes(original));

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < computedHash.Length; i++)
                {
                    sb.Append(computedHash[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
