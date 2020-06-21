using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using msTestVSnUnitVSxUnit;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.ClassLevel)]
namespace MSTestUnitTestProject
{
    [TestClass]
    public class SumTests
    {
        private IMathOperations _mathOperations;

        private Mock<ISqlRepository> _sqlRepositoryMock;

        [TestInitialize]
        public void Initialize()
        {
            _sqlRepositoryMock = new Mock<ISqlRepository>();

            _mathOperations = new MathOperations(_sqlRepositoryMock.Object);

            _sqlRepositoryMock.Setup(s => s.Create()).Returns(1);
        }

        [TestMethod]
        //[DoNotParallelize]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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
