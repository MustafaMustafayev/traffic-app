using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        public int getUserIdFromToken(string tokenString)
        {
            var jwtEncodedString = tokenString.Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            int userId = Convert.ToInt32(token.Claims.First(c => c.Type == "nameid").Value);
            return userId;
        }
    }
}
