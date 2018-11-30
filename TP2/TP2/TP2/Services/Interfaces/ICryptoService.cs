using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Services.Interfaces
{
    public interface ICryptoService
    {
        string Decrypt(string encryptedValue, string encryptionKey);
        string Encrypt(string clearValue, string encryptionKey);
        string GenerateEncryptionKey();
        string GenerateSalt();
        string HashSHA512(string value, string salt);
    }
}
