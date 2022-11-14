using SBMES.Application.Contracts;
using System.Text;

namespace SBMES.Application.Services.Encryption
{
    /// <summary>
    /// User Base64 encoding to represent serialized data
    /// </summary>
    public class Base64EncryptionService : IEncryptionService
    {
        public byte[] Encrypt(byte[] data)
        {
            var messageAsBase64String = Convert.ToBase64String(data);
            return Encoding.ASCII.GetBytes(messageAsBase64String);
        }
        public byte[] Decrypt(byte[] data)
        {
            var messageAsBase64String = Encoding.ASCII.GetString(data);
            return Convert.FromBase64String(messageAsBase64String);
        }
    }
}
