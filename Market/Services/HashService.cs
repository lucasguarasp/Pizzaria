using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Market.Services
{
    public class HashService
    {
        public static string Crip(string entrada)
        {
            var sha256Managed = new SHA256Managed();
            var bytes = Encoding.Unicode.GetBytes(entrada);
            var hash = sha256Managed.ComputeHash(bytes);
            return hash.Aggregate(string.Empty, (current, x) => current + $"{x:x2}");
        }
    }
}
