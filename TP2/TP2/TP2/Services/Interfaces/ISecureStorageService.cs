using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TP2.Services.Interfaces
{
    public interface ISecureStorageService
    {
        Task<string> GetEncryptionKeyAsync(string keyId);
        Task SetEncryptionKeyAsync(string keyId, string keyValue);
    }
}
