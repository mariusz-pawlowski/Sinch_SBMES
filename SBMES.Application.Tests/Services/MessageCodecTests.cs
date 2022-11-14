using SBMES.Application.Models;
using SBMES.Application.Services;
using Xunit.Sdk;

namespace SBMES.Application.Tests.Services
{
    public class MessageCodecTests
    {
        [Fact]
        public void Encode_WhenMessageIsValid_AfterDecodingMessagesShouldBeEqual()
        {
            //Arrange
            var sut = new MessageCodec();

            var headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("header1", "value1"));
            headers.Add(new KeyValuePair<string, string>("header2", "value2"));

            var payload = new byte[] { 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26 };

            var testMessage = new Message(headers, payload);
            //Act
            var encodedMessage = sut.Encode(testMessage);

            var decodedMessage = sut.Decode(encodedMessage);

            //Assert
            testMessage.Headers.Count.Should().Be(decodedMessage.Headers.Count);
            testMessage.Headers.First().Key.Should().Be(decodedMessage.Headers.First().Key);
            testMessage.Headers.First().Value.Should().Be(decodedMessage.Headers.First().Value);
            testMessage.Payload.Count().Should().Be(decodedMessage.Payload.Count());
            testMessage.Payload[1].Should().Be(decodedMessage.Payload[1]);
        }

        [Fact]
        public void Encode_LoadImageAsPayload_AfterDecodingFileIsAsOriginal()
        {
            //Arrange
            var sut = new MessageCodec();

            var headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("header1", "value1"));
            var payload = FileToByteArray("./TestFiles/sample.jpeg");

            var testMessage = new Message(headers, payload);
            //Act
            var encodedMessage = sut.Encode(testMessage);
            var decodedMessage = sut.Decode(encodedMessage);

            //Assert
            testMessage.Headers.Count.Should().Be(decodedMessage.Headers.Count);
            testMessage.Payload.Count().Should().Be(decodedMessage.Payload.Count());
        }

        [Fact(Skip = "Run this test manually if needed")]
        //[Fact]
        public void Encode_LoadImageAsPayload_AfterDecodingNewlyCreatedImageIsTheSameAsOriginal()
        {
            //Arrange
            var sut = new MessageCodec();

            var headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("header1", "value1"));

            var payload = FileToByteArray("./TestFiles/sample.jpeg");

            var testMessage = new Message(headers, payload);
            //Act
            var encodedMessage = sut.Encode(testMessage);

            var decodedMessage = sut.Decode(encodedMessage);

            //Assert
            using var writer = new BinaryWriter(File.OpenWrite("./TestFiles/" + DateTime.Now.ToFileTime() + "_sample.jpeg"));
            writer.Write(decodedMessage.Payload); 
            //verify manually newly created file on disk
        }

        private byte[] FileToByteArray(string fileName)
        {
            byte[] fileData;

            using (FileStream fs = File.OpenRead(fileName))
            {
                using (BinaryReader binaryReader = new(fs))
                {
                    fileData = binaryReader.ReadBytes((int)fs.Length);
                }
            }
            return fileData;
        }
    }
}
