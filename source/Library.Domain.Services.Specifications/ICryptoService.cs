using TDCore.Core;
using Library.Domain.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Services.Specifications
{
    [ObjectMap("CryptoService", true)]
    public interface ICryptoService
    {
        string Encrypt(object data);
        string Decrypt(string cryptString);
    }
}
