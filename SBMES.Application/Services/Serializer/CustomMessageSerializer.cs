using AutoMapper;
using SBMES.Application.AutoMaper;
using SBMES.Application.Contracts;
using SBMES.Application.Models;
using System.Text;

namespace SBMES.Application.Services.Serializer
{
    public class CustomMessageSerializer : IMessageSerializer
    {
        private readonly IMapper iMapper;

        public CustomMessageSerializer()
        {
            var config = new AutoMapperConfiguration().Configure();
            iMapper = config.CreateMapper();
        }

        public byte[] Serialize(Message message)
        {
            var messageFlat = iMapper.Map<MessageStruct>(message);
            var headersBytes = Encoding.Unicode.GetBytes(messageFlat.Headers);
            var headersBytesSize = headersBytes.Length;

            byte[] result = CombineArrays(BitConverter.GetBytes(headersBytesSize), headersBytes, messageFlat.Payload);
            return result;
        }
        public Message Deserialize(byte[] data)
        {

            var headersBytesSize = BitConverter.ToInt32(data[0..sizeof(int)], 0);
            var headersBytes = data[sizeof(int)..(sizeof(int) + headersBytesSize)];
            var headers = Encoding.Unicode.GetString(headersBytes);

            var payload = data.Skip(sizeof(int) + headersBytes.Length).ToArray();

            var messageStruct = new MessageStruct(headers, payload);
            var message = iMapper.Map<Message>(messageStruct);
            return message;
        }

        private static byte[] CombineArrays(params byte[][] arrays)
        {
            byte[] ret = new byte[arrays.Sum(x => x.Length)];
            int offset = 0;
            foreach (byte[] data in arrays)
            {
                Buffer.BlockCopy(data, 0, ret, offset, data.Length);
                offset += data.Length;
            }
            return ret;
        }
    }
}
