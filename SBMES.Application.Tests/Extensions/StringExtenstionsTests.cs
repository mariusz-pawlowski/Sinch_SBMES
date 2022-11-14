
using SBMES.Application.Extenstions;

namespace SBMES.Application.Tests.Extensions
{
   
    public class StringExtenstionsTests
    {
        [Fact]
        public void IsASCII_WhenStringContainsOnlyASCIIChars_ShouldReturnTrue()
        {
            //Arrange
            string valueToTest = "test123TEST$!";
            //Act
            var result = valueToTest.IsASCII();

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsASCII_WhenStringContainsOnlyASCIIChars_ShouldReturnFalse()
        {
            //Arrange
            string valueToTest = "Ä€ą";
            //Act
            var result = valueToTest.IsASCII();

            //Assert
            result.Should().BeFalse();
        }
    }
}
