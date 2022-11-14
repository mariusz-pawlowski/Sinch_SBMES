using SBMES.Application.Contracts;
using SBMES.Application.Models;
using SBMES.Application.Services.Serializer;

namespace SBMES.Application.Tests.Services.Serializer
{
    public class CustomMessageSerializerTests
    {
        private readonly IMessageSerializer sut;

        public CustomMessageSerializerTests()
        {
            sut = new CustomMessageSerializer();
        }

        [Fact]
        public void Serialize_WhenEmptyHeaders_ShouldReturnPayloadPlus4bytes()
        {
            //Arrange
            var headers = new List<KeyValuePair<string, string>>();
            var payload = new byte[] { 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26 };
            var testMessage = new Message(headers, payload);

            //Act
            var result = sut.Serialize(testMessage);

            //Assert
            result.Length.Should().Be(payload.Length + sizeof(int));
        }

        [Fact]
        public void Deserialize_ShouldEqualOriginalMessage()
        {
            //Arrange
            var headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("header1", "value1"));
            headers.Add(new KeyValuePair<string, string>("header2", "value2"));
            var payload = new byte[] { 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26 };
            var testMessage = new Message(headers, payload);

            //Act
            var serializedMessage = sut.Serialize(testMessage);
            var deserializedMessage = sut.Deserialize(serializedMessage);

            //Assert
            testMessage.Headers.Count().Should().Be(deserializedMessage.Headers.Count());
            testMessage.Headers.First().Key.Should().Be(deserializedMessage.Headers.First().Key);
            testMessage.Payload.Length.Should().Be(deserializedMessage.Payload.Length);
        }
    }
}
