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
            (double, double) pay = (47500, 19000);

            // Act
            var x = sut.CalculateEmployeePay(perm.GetIDfromIndex(1));

            // Assert
            Assert.That(x, Is.EqualTo(pay));
        }
    }
}
