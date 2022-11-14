
using AutoMapper;
using Newtonsoft.Json.Linq;
using SBMES.Application.AutoMaper;
using SBMES.Application.Models;
using SBMES.Application.Services;


namespace SBMES.Application.Tests.AutoMapper
{
    public class MessageMappingProfileTests
    {
        private readonly IMapper sut;

        public MessageMappingProfileTests()
        {
            var config = new AutoMapperConfiguration().Configure();
            sut = config.CreateMapper();
        }
       
        [Fact]
        public void MapMessageToMessageStruct_ShouldEncodeHeader()
        {
            //Arrange
           

            var payload = Array.Empty<byte>();

            var headers = new List<KeyValuePair<string, string>>();
            headers.Add(new KeyValuePair<string, string>("header1", "value1"));
            headers.Add(new KeyValuePair<string, string>("header2", "value2"));
            var testMessage = new Message(headers, payload);

            //Act
            var messageStruct = sut.Map<MessageStruct>(testMessage);

            //Assert
            messageStruct.Headers.Should().Be("header1Ωvalue1Σheader2Ωvalue2");
        }

        [Fact]
        public void MapMessageStructToMessage_ShouldDecodeHeader()
        {
            //Arrange
            var testMessageStruct = new MessageStruct("header1Ωvalue1Σheader2Ωvalue2", Array.Empty<byte>());

            //Act
            var messageStruct = sut.Map<Message>(testMessageStruct);

            //Assert
            messageStruct.Headers.Count.Should().Be(2);
            messageStruct.Headers.First().Key.Should().Be("header1");
            messageStruct.Headers.First().Value.Should().Be("value1");
        }
    }
}
