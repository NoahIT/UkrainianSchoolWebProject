using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Subscriptions.Payments
{
    public interface ISignatureGenerator
    {
        string GenerateSignature(params string[] data);
    }
}
