using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBMES.Application.Validators;
using Models = SBMES.Application.Models;

namespace SBMES.Application.Tests.Validators
{
    public class MessageValidatorText
    {
        [Fact]
        public void IsValid_WhenHeaderHasNumberOfRecordsGreaterThan63_ShouldReturnFalse()
        {
            //Arrange
            var sut = new MessageValidator();
            var message = new Models.Message(Enumerable.Range(1, 64).Select(i => new KeyValuePair<string, string>("header" + i, "header_value")).ToList(), Array.Empty<byte>());

            //Act
            var validationResult = sut.Validate(message);

            //Assert
            validationResult.IsValid.Should().BeFalse();
        }
    }
}
