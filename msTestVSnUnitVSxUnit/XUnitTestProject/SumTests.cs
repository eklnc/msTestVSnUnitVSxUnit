using Moq;
using msTestVSnUnitVSxUnit;
using Xunit;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass)]
namespace XUnitTestsProject
{
    public class SumTests
    {
        private IMathOperations _mathOperations;

        private Mock<ISqlRepository> _sqlRepositoryMock;

        public SumTests()
        {
            _sqlRepositoryMock = new Mock<ISqlRepository>();

            _mathOperations = new MathOperations(_sqlRepositoryMock.Object);

            _sqlRepositoryMock.Setup(s => s.Create()).Returns(1);
        }

        [Fact]
        public void Should_Sum_When_ValidParameters()
        {
            // case 1
            var param1 = 5;
            var param2 = 10;

            int? result = _mathOperations.Sum(param1, param2);
            Assert.True(result.HasValue && result.Value == 15);

            // case 2
            param1 = -5;
            param2 = 10;

            result = _mathOperations.Sum(param1, param2);
            Assert.True(result.HasValue && result.Value == 5);
        }

        [Fact]
        public void Should_Not_Sum_When_NullParameter()
        {
            // case 1
            int? param1 = null;
            int? param2 = 10;

            int? result = _mathOperations.Sum(param1, param2);
            Assert.True(!result.HasValue);

            // case 2
            param1 = -5;
            param2 = null;

            result = _mathOperations.Sum(param1, param2);
            Assert.True(!result.HasValue);
        }

        [Fact]
        public void Should_Not_Sum_When_ParameterIntMaxValue()
        {
            // case 1
            int? param1 = int.MaxValue;
            int? param2 = 10;

            int? result = _mathOperations.Sum(param1, param2);
            Assert.True(!result.HasValue);

            // case 2
            param1 = -5;
            param2 = int.MaxValue;

            result = _mathOperations.Sum(param1, param2);
            Assert.True(!result.HasValue);
        }

        [Fact]
        public void Should_Not_Sum_When_SqlCreateReturnZero()
        {
            _sqlRepositoryMock.Setup(s => s.Create()).Returns(0);

            int? param1 = 5;
            int? param2 = 10;

            int? result = _mathOperations.Sum(param1, param2);
            Assert.True(!result.HasValue);
        }
    }
}
