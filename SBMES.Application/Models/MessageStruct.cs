using System.Runtime.InteropServices;

namespace SBMES.Application.Models
{
    /// <summary>
    /// Flat structure of <ref>Message</ref> in a form of value objects
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public struct MessageStruct
    {
        public MessageStruct(string headers, byte[] payload)
        {
            Headers = headers;
            Payload = payload;
        }
        public string Headers;
        public byte[] Payload;
    }
}
