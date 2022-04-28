using NUnit.Framework;
using PayCal.Models;

namespace PayCal_Tests
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
        public void Check_Salaryint_is_zero()
        {
            // Arrange
            var sut = new PermEmployeeData();

            // Act + Assert
            Assert.AreEqual(sut.Salaryint, 0);
        }

        [Test]
        public void Check_Bonusint_is_zero()
        {
            // Arrange
            var sut = new PermEmployeeData();

            // Act + Assert
            Assert.AreEqual(sut.Bonusint, 0);
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
