using NUnit.Framework;
using PayCal.Models;

namespace PayCal_Tests
{
    [TestFixture]
    public class TempEmployeeDataShould
    {
        [Test]
        public void Check_FName_is_null()
        {
            // Arrange
            var sut = new TempEmployeeData();

            // Act + Assert
            Assert.Null(sut.FName);
        }

        [Test]
        public void Check_LName_is_null()
        {
            // Arrange
            var sut = new TempEmployeeData();

            // Act + Assert
            Assert.Null(sut.LName);
        }

        [Test]
        public void Check_DayRateint_is_zero()
        {
            // Arrange
            var sut = new TempEmployeeData();

            // Act + Assert
            Assert.AreEqual(sut.DayRateint, 0);
        }

        [Test]
        public void Check_WeeksWorkedint_is_zero()
        {
            // Arrange
            var sut = new TempEmployeeData();

            // Act + Assert
            Assert.AreEqual(sut.WeeksWorkedint, 0);
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
