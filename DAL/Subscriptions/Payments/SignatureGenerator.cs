using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Subscriptions.Payments
{
    public class SignatureGenerator : ISignatureGenerator
    {
        private readonly string _secretKey;

        public SignatureGenerator(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string GenerateSignature(params string[] data)
        {
            foreach (var item in data)
            {
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(data), "One of the parameters is null.");
                }
            }

            var dataString = string.Join(";", data);
            using (var hmac = new HMACMD5(Encoding.UTF8.GetBytes(_secretKey)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataString));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
