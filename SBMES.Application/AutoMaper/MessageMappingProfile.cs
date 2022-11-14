using AutoMapper;
using System.Text;
using SBMES.Application.Models;

namespace SBMES.Application.AutoMaper
{
    public class MessageMappingProfile : Profile
    {
        private const string KeyValueSeparator = "Ω"; //non ASCII char, can act as separator
        private const string HeadersSeparator = "Σ";

        public MessageMappingProfile()
        {
            CreateMap<List<KeyValuePair<string, string>>,string>()
                .ConvertUsing(src => SerializeMessageHeader(src));

            CreateMap<string, List<KeyValuePair<string, string>>>()
                .ConvertUsing(src => DeserializeMessageHeaders(src));

            CreateMap<MessageStruct, Message>().ReverseMap();
        }

        private static string SerializeMessageHeader(List<KeyValuePair<string,string>> headers)
        {
            // Code for deserializing
            StringBuilder headerSB = new();
            foreach (var header in headers)
            {
                headerSB.Append($"{header.Key}{KeyValueSeparator}{header.Value}");
                if (headers.IndexOf(header) != headers.Count - 1)// not last item
                {
                    headerSB.Append(HeadersSeparator);
                }
            }
            return headerSB.ToString();
        }

        private static List<KeyValuePair<string, string>> DeserializeMessageHeaders(string headers)
        {
            var list = new List<KeyValuePair<string, string>>();
            foreach (var header in headers.Split(HeadersSeparator))
            {
                var keyValue = header.Split(KeyValueSeparator);
                if(keyValue.Length == 2)
                list.Add(new KeyValuePair<string, string>(keyValue[0], keyValue[1]));
            }
            return list;
        }
    }
}
