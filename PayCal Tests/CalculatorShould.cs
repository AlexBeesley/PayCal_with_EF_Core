using NUnit.Framework;
using PayCal.Models;
using PayCal.Repositories;
using PayCal.Services;


namespace PayCal___Tests
{
    [TestFixture]
    public class CalculatorShould
    {
        [Test]
        public void Check_CalculateEmployeePay_method_returns_correct_pay()
        {
            // Arrange
            IRepository<PermEmployeeData> perm = new PermEmployeeRepository();
            IRepository<TempEmployeeData> temp = new TempEmployeeRepository();
            Calculator sut = new Calculator(perm, temp);

            // Act
            var x = sut.CalculateEmployeePay(perm.GetIDfromIndex(1));

            // Assert
            Assert.That(x, Is.EqualTo(19000));
        }

        [Test]
        public void Check_CalculateTaxBands_returns_correct_answer()
        {
            // Arrange
            IRepository<PermEmployeeData> perm = new PermEmployeeRepository();
            IRepository<TempEmployeeData> temp = new TempEmployeeRepository();
            Calculator sut = new Calculator(perm, temp);

            // Act
            var x = sut.CalculateTaxBands(23000);

            // Assert
            Assert.That(x, Is.EqualTo(0.2));
        }
    }
}
