using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TP2.Services
{
    public async Task<string> GetEncryptionKeyAsync(string keyId)
    {
        try
        {
            return await SecureStorage.GetAsync(keyId);
        }
        catch
        {
            throw new ArgumentNullException(null, "Error");
        }
    }

    public async Task SetEncryptionKeyAsync(string keyId, string keyValue)
    {
        try
        {
            await SecureStorage.SetAsync(keyId, keyValue);
        }
        catch
        {
            throw new ArgumentNullException();
        }
    }
}
}
