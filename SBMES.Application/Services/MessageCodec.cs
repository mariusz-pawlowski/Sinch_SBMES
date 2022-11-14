using SBMES.Application.Contracts;
using SBMES.Application.Models;
using SBMES.Application.Validators;
using SBMES.Application.Exceptions;
using SBMES.Application.Services.Serializer;
using SBMES.Application.Services.Encryption;

namespace SBMES.Application.Services
{
    /// <summary>
    /// Message codec.
    /// </summary>
    public class MessageCodec : IMessageCodec
    {
        private static readonly MessageValidator _messageValidator = new();
        private readonly IMessageSerializer _messageSerializer;
        private readonly IEncryptionService _encryptionService;

        public MessageCodec()
        {
            _encryptionService = new DESEncryptionService();
            _messageSerializer = new CustomMessageSerializer();
        }

        public MessageCodec(IEncryptionService encryptionService, IMessageSerializer messageSerializer)
        {
            _messageSerializer = messageSerializer;
            _encryptionService = encryptionService;
        }

        /// <summary>
        /// Encodes message to binary format.
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Encoded message in binary format</returns>
        /// <exception cref="ValidationException">Thrown when message is not in valid format or does not met business rules.</exception>
        public byte[] Encode(Message message)
        {
            var validateMessage = _messageValidator.Validate(message);
            if (!validateMessage.IsValid)
            {
                throw new ValidationException(validateMessage.Errors);
            }

            var messageSerialized = _messageSerializer.Serialize(message);
            var messageEncrypted = _encryptionService.Encrypt(messageSerialized);
            return messageEncrypted;
        }

        /// <summary>
        /// Decodes message from binary format.
        /// </summary>
        /// <param name="data">Encoded message in binary format</param>
        /// <returns>Message decoded from binary format</returns>
        public Message Decode(byte[] data)
        {
            var messageDecrypted = _encryptionService.Decrypt(data);
            var message = _messageSerializer.Deserialize(messageDecrypted);
            
            return message;
        }
    }
}
