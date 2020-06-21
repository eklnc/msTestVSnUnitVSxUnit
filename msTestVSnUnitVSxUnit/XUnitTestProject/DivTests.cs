using Moq;
using msTestVSnUnitVSxUnit;
using Xunit;

namespace XUnitTestsProject
{
    public class DivTests
    {
        private IMathOperations _mathOperations;

        private Mock<ISqlRepository> _sqlRepositoryMock;

        public DivTests()
        {
            _sqlRepositoryMock = new Mock<ISqlRepository>();

            _mathOperations = new MathOperations(_sqlRepositoryMock.Object);

            _sqlRepositoryMock.Setup(s => s.Delete()).Returns(1);
        }

        [Fact]
        public void Should_Div_When_ValidParameters()
        {
            // case 1
            var param1 = 10;
            var param2 = 5;

            int? result = _mathOperations.Div(param1, param2);
            Assert.True(result.HasValue && result.Value == 2);
        }

        [Fact]
        public void Should_Not_Div_When_NullParameter()
        {
            // case 1
            int? param1 = null;
            int? param2 = 10;

            int? result = _mathOperations.Div(param1, param2);
            Assert.True(!result.HasValue);

            // case 2
            param1 = -5;
            param2 = null;

            result = _mathOperations.Div(param1, param2);
            Assert.True(!result.HasValue);
        }

        [Fact]
        public void Should_Not_Div_When_ParameterIntMaxValue()
        {
            // case 1
            int? param1 = int.MaxValue;
            int? param2 = 10;

            int? result = _mathOperations.Div(param1, param2);
            Assert.True(!result.HasValue);

            // case 2
            param1 = -5;
            param2 = int.MaxValue;

            result = _mathOperations.Div(param1, param2);
            Assert.True(!result.HasValue);
        }

        [Fact]
        public void Should_Not_Div_When_SecondParameterShouldBeSmall()
        {
            int? param1 = 5;
            int? param2 = 10;

            int? result = _mathOperations.Div(param1, param2);
            Assert.True(!result.HasValue);
        }

        [Fact]
        public void Should_Not_Div_When_SecondParameterCannotBeZero()
        {
            int? param1 = 5;
            int? param2 = 0;

            int? result = _mathOperations.Div(param1, param2);
            Assert.True(!result.HasValue);
        }

        [Fact]
        public void Should_Not_Div_When_ReminderMustBeZero()
        {
            int? param1 = 10;
            int? param2 = 3;

            int? result = _mathOperations.Div(param1, param2);
            Assert.True(!result.HasValue);
        }

        [Fact]
        public void Should_Not_Div_When_SqlDeleteReturnZero()
        {
            _sqlRepositoryMock.Setup(s => s.Delete()).Returns(0);

            int? param1 = 10;
            int? param2 = 5;

            int? result = _mathOperations.Div(param1, param2);
            Assert.True(!result.HasValue);
        }
    }
}
