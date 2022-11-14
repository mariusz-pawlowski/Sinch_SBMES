# Simple Binary Message Encoding Scheme

SBMES is an SDK that allows to encode/decode custom messages.

## How to run it 

_Prerrequisites_
- .net core 6.0 runtime installed
- .net IDE like VS Code or VisualStudio

_How to test it_ 
- build project in visual studio or  using ```dotnet build``` command
- copy dll from bin and reference it
- build SBMES.Application to nuget (project properties -> packaging -> general) and then refrence it
- run one of many unit tests  i.e [Encode_LoadImageAsPayload_AfterDecodingNewlyCreatedImageIsTheSameAsOriginal](https://github.com/mariusz-pawlowski/Sinch_SBMES/blob/main/SBMES.Application.Tests/Services/MessageCodecTests.cs#L57) UnitTest encodes message with image as payload, then decodes it and recreates encoded image

##  How does it works 
**[Encode](https://github.com/mariusz-pawlowski/Sinch_SBMES/blob/main/SBMES.Application/Services/MessageCodec.cs#L37)** consists of 3 steps

#### Validation
- to validate [Message](https://github.com/mariusz-pawlowski/Sinch_SBMES/blob/main/SBMES.Application/Models/Message.cs) _Rules_ desing pattern is used
- to make implementation clean, [FluentValidadion](https://www.nuget.org/packages/fluentvalidation/) nuget package was introduced
- all  business validation rules can be found in [MessageValidator](https://github.com/mariusz-pawlowski/Sinch_SBMES/blob/main/SBMES.Application/Validators/MessageValidator.cs) and [HeaderValidator ](https://github.com/mariusz-pawlowski/Sinch_SBMES/blob/main/SBMES.Application/Validators/HeaderValidator.cs)
- if vaildation fails then all errors are groupped together and [ValidationException](https://github.com/mariusz-pawlowski/Sinch_SBMES/blob/main/SBMES.Application/Services/MessageCodec.cs#L42) gets thrown

#### Serialization
- Message serialization to binary format takes place in  [CustomMessageSerializer](https://github.com/mariusz-pawlowski/Sinch_SBMES/blob/main/SBMES.Application/Services/Serializer/CustomMessageSerializer.cs)
- first Message object is flattened through Automapper [MessageMappingProfile](https://github.com/mariusz-pawlowski/Sinch_SBMES/blob/main/SBMES.Application/Models/MessageStruct.cs) to [MessageStruct](https://github.com/mariusz-pawlowski/Sinch_SBMES/blob/main/SBMES.Application/Models/MessageStruct.cs) set. During that process original headers are converted to single string.
- then we merge headers&payload into one byte array. To make deserialization possible at the beginning of new array we but few additional bytes ([details](https://github.com/mariusz-pawlowski/Sinch_SBMES/blob/main/SBMES.Application/Services/Serializer/CustomMessageSerializer.cs#L25))

#### Encryption
- as message needs to be decoded, we need two-way encryption algorithm
- default encryption can be found in [DESEncryptionService ](https://github.com/mariusz-pawlowski/Sinch_SBMES/blob/main/SBMES.Application/Services/Encryption/DESEncryptionService.cs), where DES algorithm is used. [Seed](https://github.com/mariusz-pawlowski/Sinch_SBMES/blob/main/SBMES.Application/Services/Encryption/DESEncryptionService.cs#L14) is kept in memory as SecureString in memory.
- there is also simpler algorithm [Base64EncryptionService ](https://github.com/mariusz-pawlowski/Sinch_SBMES/blob/main/SBMES.Application/Services/Encryption/Base64EncryptionService.cs) that uses Base64 binary-to-text encoding, but without salting

**[Decode](https://github.com/mariusz-pawlowski/Sinch_SBMES/blob/main/SBMES.Application/Services/MessageCodec.cs#L55)** is a reverse process to encoding and consists of 2 steps
#### Decryption
- [Decrypt](https://github.com/mariusz-pawlowski/Sinch_SBMES/blob/main/SBMES.Application/Services/Encryption/DESEncryptionService.cs#L31)

#### Deserialization
- [Deserialize](https://github.com/mariusz-pawlowski/Sinch_SBMES/blob/main/SBMES.Application/Services/Serializer/CustomMessageSerializer.cs#L28)