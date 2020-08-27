using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CalibrationSoftware.Models
{
    public class UserToken
    {
        public string UserName { get; set; }
        public string Token { get; set; }

        public UserToken generateUSertoken(string username)
        {
            DateTime dt = DateTime.Now;
            long nowAsLong = dt.ToBinary();
            byte[] nowByttes = BitConverter.GetBytes(nowAsLong);

            byte[] message = Encoding.ASCII.GetBytes(username);
            HMACSHA256 hash = new HMACSHA256(nowByttes);

            byte[] token = hash.ComputeHash(message);

            UserToken token1 = new UserToken();
            token1.UserName = username;
            token1.Token = string.Concat(token.Select(x => x.ToString()));

            return token1;
        }
    }
}