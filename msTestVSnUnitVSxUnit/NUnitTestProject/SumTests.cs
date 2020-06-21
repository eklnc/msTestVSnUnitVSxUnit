using Moq;
using msTestVSnUnitVSxUnit;
using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]
namespace NUnitTestProject
{
    [TestFixture]
    public class SumTests
    {
        private IMathOperations _mathOperations;

        private Mock<ISqlRepository> _sqlRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _sqlRepositoryMock = new Mock<ISqlRepository>();

            _mathOperations = new MathOperations(_sqlRepositoryMock.Object);

            _sqlRepositoryMock.Setup(s => s.Create()).Returns(1);
        }

        [Test]
        //[NonParallelizable]
        public void Should_Sum_When_ValidParameters()
        {
            // case 1
            var param1 = 5;
            var param2 = 10;

            int? result = _mathOperations.Sum(param1, param2);
            Assert.IsTrue(result.HasValue && result.Value == 15);

            // case 2
            param1 = -5;
            param2 = 10;

            result = _mathOperations.Sum(param1, param2);
            Assert.IsTrue(result.HasValue && result.Value == 5);
        }

        [Test]
        public void Should_Not_Sum_When_NullParameter()
        {
            // case 1
            int? param1 = null;
            int? param2 = 10;

            int? result = _mathOperations.Sum(param1, param2);
            Assert.IsTrue(!result.HasValue);

            // case 2
            param1 = -5;
            param2 = null;

            result = _mathOperations.Sum(param1, param2);
            Assert.IsTrue(!result.HasValue);
        }

        [Test]
        public void Should_Not_Sum_When_ParameterIntMaxValue()
        {
            // case 1
            int? param1 = int.MaxValue;
            int? param2 = 10;

            int? result = _mathOperations.Sum(param1, param2);
            Assert.IsTrue(!result.HasValue);

            // case 2
            param1 = -5;
            param2 = int.MaxValue;

            result = _mathOperations.Sum(param1, param2);
            Assert.IsTrue(!result.HasValue);
        }

        [Test]
        public void Should_Not_Sum_When_SqlCreateReturnZero()
        {
            _sqlRepositoryMock.Setup(s => s.Create()).Returns(0);

            int? param1 = 5;
            int? param2 = 10;

            int? result = _mathOperations.Sum(param1, param2);
            Assert.IsTrue(!result.HasValue);
        }
    }
}