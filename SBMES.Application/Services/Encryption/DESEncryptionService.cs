using SBMES.Application.Contracts;
using SBMES.Application.Extenstions;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace SBMES.Application.Services.Encryption
{
    /// <summary>
    /// Uses DES algorythm for two-way encryption.
    /// </summary>
    public class DESEncryptionService : IEncryptionService
    {
        private readonly SecureString encryptionSeed = "!@#seed$%^".ToSecureString();
        private readonly TripleDES tripleDesCryptoServiceProvider;

        public DESEncryptionService()
        {
            tripleDesCryptoServiceProvider = TripleDES.Create();
            using MD5CryptoServiceProvider mD5CryptoServiceProvider = new();
            tripleDesCryptoServiceProvider.Key = mD5CryptoServiceProvider.ComputeHash(Encoding.Unicode.GetBytes(encryptionSeed.ToUnsecureString()));
            tripleDesCryptoServiceProvider.Mode = CipherMode.ECB;
        }

        public byte[] Encrypt(byte[] data)
        {
            using var cryptoTransformEncryptor = tripleDesCryptoServiceProvider.CreateEncryptor();
            return cryptoTransformEncryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Decrypt(byte[] data)
        {
            using var cryptoTransformDecryptor = tripleDesCryptoServiceProvider.CreateDecryptor();
            return cryptoTransformDecryptor.TransformFinalBlock(data, 0, data.Length);
        }


    }
}
