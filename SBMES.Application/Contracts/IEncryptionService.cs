namespace SBMES.Application.Contracts
{
    public interface IEncryptionService
    {
        public byte[] Encrypt(byte[] data);
        public byte[] Decrypt(byte[] data);
    }
}
