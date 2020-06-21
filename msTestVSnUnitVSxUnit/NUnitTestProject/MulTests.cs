using Moq;
using msTestVSnUnitVSxUnit;
using NUnit.Framework;
using System.Collections.Generic;

namespace NUnitTestProject
{
    [TestFixture]
    public class MulTests
    {
        private IMathOperations _mathOperations;

        private Mock<ISqlRepository> _sqlRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _sqlRepositoryMock = new Mock<ISqlRepository>();

            _mathOperations = new MathOperations(_sqlRepositoryMock.Object);

            _sqlRepositoryMock.Setup(s => s.Read()).Returns(new List<string> { "test" });
        }

        [Test]
        public void Should_Mul_When_ValidParameters()
        {
            // case 1
            var param1 = 5;
            var param2 = 10;

            int? result = _mathOperations.Mul(param1, param2);
            Assert.IsTrue(result.HasValue && result.Value == 50);

            // case 2
            param1 = -5;
            param2 = 10;

            result = _mathOperations.Mul(param1, param2);
            Assert.IsTrue(result.HasValue && result.Value == -50);
        }

        [Test]
        public void Should_Not_Mul_When_NullParameter()
        {
            // case 1
            int? param1 = null;
            int? param2 = 10;

            int? result = _mathOperations.Mul(param1, param2);
            Assert.IsTrue(!result.HasValue);

            // case 2
            param1 = -5;
            param2 = null;

            result = _mathOperations.Mul(param1, param2);
            Assert.IsTrue(!result.HasValue);
        }

        [Test]
        public void Should_Not_Mul_When_ParameterIntMaxValue()
        {
            // case 1
            int? param1 = int.MaxValue;
            int? param2 = 10;

            int? result = _mathOperations.Mul(param1, param2);
            Assert.IsTrue(!result.HasValue);

            // case 2
            param1 = -5;
            param2 = int.MaxValue;

            result = _mathOperations.Mul(param1, param2);
            Assert.IsTrue(!result.HasValue);
        }

        [Test]
        public void Should_Not_Mul_When_SqlReadReturnEmpty()
        {
            _sqlRepositoryMock.Setup(s => s.Read()).Returns(new List<string>());

            int? param1 = 5;
            int? param2 = 10;

            int? result = _mathOperations.Mul(param1, param2);
            Assert.IsTrue(!result.HasValue);
        }
    }
}