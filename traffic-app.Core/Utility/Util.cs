using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace traffic_app.Core.Utility
{
    public class Util : IUtil
    {
        public string GetHash(string value)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
                string hashedValue = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hashedValue;
            }
        }
    }
}
