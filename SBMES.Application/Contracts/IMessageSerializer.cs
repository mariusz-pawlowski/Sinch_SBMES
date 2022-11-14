using SBMES.Application.Models;

namespace SBMES.Application.Contracts
{
    public interface IMessageSerializer
    {
        public byte[] Serialize(Message mesage);
        public Message Deserialize(byte[] data);
    }
}
