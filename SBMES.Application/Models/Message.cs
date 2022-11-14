namespace SBMES.Application.Models
{
    /// <summary>
    /// Message
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Set of headers
        /// 1. A message can contain a variable number of headers, and a binary payload.
        /// 2. The headers are name-value pairs, where both names and values are ASCII-encoded strings.
        /// 3. Header names and values are limited to 1023 bytes (independently).
        /// 4. A message can have max 63 headers.
        /// </summary>
        public List<KeyValuePair<string, string>> Headers { get; set; }
        /// <summary>
        /// Message Payload
        /// 1. The message payload is limited to 256 KiB
        /// </summary>
        public byte[] Payload { get; set; }

        public Message(List<KeyValuePair<string, string>> headers, byte[] payload)
        {
            Headers = headers;
            Payload = payload;
        }
    }
}
