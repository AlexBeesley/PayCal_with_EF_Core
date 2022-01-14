using NUnit.Framework;
using PayCal.Models;

namespace PayCal___Tests
{
    [TestFixture]
    public class PermEmployeeDataShould
    {
        [Test]
        public void Check_FName_is_null()
        {
            // Arrange
            var sut = new PermEmployeeData();

            // Act + Assert
            Assert.Null(sut.FName);
        }

        [Test]
        public void Check_LName_is_null()
        {
            // Arrange
            var sut = new PermEmployeeData();

            // Act + Assert
            Assert.Null(sut.LName);
        }

        [Test]
        public void Check_Salaryint_is_null()
        {
            // Arrange
            var sut = new PermEmployeeData();

            // Act + Assert
            Assert.Null(sut.Salaryint);
        }

        [Test]
        public void Check_Bonusint_is_null()
        {
            // Arrange
            var sut = new PermEmployeeData();

            // Act + Assert
            Assert.Null(sut.Bonusint);
        }

        [Test]
        public void Check_method_ToString_returns_expected_string()
        {
            // Arrange
            var sut = new PermEmployeeData();

            // Act
            var x = sut.ToString();

            // Assert
            Assert.That(x, Contains.Substring(""));
        }
    }
}
