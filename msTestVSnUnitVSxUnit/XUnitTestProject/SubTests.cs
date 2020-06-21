using Moq;
using msTestVSnUnitVSxUnit;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestsProject
{
    public class SubTests
    {
        private IMathOperations _mathOperations;

        private Mock<ISqlRepository> _sqlRepositoryMock;

        public SubTests()
        {
            _sqlRepositoryMock = new Mock<ISqlRepository>();

            _mathOperations = new MathOperations(_sqlRepositoryMock.Object);

            _sqlRepositoryMock.Setup(s => s.Update()).Returns(1);
        }

        [Fact]
        public void Should_Sub_When_ValidParameters()
        {
            // case 1
            var param1 = 10;
            var param2 = 5;

            int? result = _mathOperations.Sub(param1, param2);
            Assert.True(result.HasValue && result.Value == 5);

            // case 2
            param1 = -5;
            param2 = -10;

            result = _mathOperations.Sub(param1, param2);
            Assert.True(result.HasValue && result.Value == 5);
        }

        [Fact]
        public void Should_Not_Sub_When_NullParameter()
        {
            // case 1
            int? param1 = null;
            int? param2 = 10;

            int? result = _mathOperations.Sub(param1, param2);
            Assert.True(!result.HasValue);

            // case 2
            param1 = -5;
            param2 = null;

            result = _mathOperations.Sub(param1, param2);
            Assert.True(!result.HasValue);
        }

        [Fact]
        public void Should_Not_Sub_When_ParameterIntMaxValue()
        {
            // case 1
            int? param1 = int.MaxValue;
            int? param2 = 10;

            int? result = _mathOperations.Sub(param1, param2);
            Assert.True(!result.HasValue);

            // case 2
            param1 = -5;
            param2 = int.MaxValue;

            result = _mathOperations.Sub(param1, param2);
            Assert.True(!result.HasValue);
        }

        [Fact]
        public void Should_Not_Sub_When_SecondParameterShouldBeSmall()
        {
            int? param1 = 5;
            int? param2 = 10;

            int? result = _mathOperations.Sub(param1, param2);
            Assert.True(!result.HasValue);
        }

        [Fact]
        public void Should_Not_Sub_When_SqlUpdateReturnZero()
        {
            _sqlRepositoryMock.Setup(s => s.Update()).Returns(0);

            int? param1 = 10;
            int? param2 = 5;

            int? result = _mathOperations.Sub(param1, param2);
            Assert.True(!result.HasValue);
        }
    }
}
