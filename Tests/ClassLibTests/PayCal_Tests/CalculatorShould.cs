using Moq;
using NUnit.Framework;
using PayCal.Models;
using PayCal.Repositories;
using PayCal.Repositories.Volatile;
using PayCal.Services;

namespace PayCal_Tests
{
    [TestFixture]
    public class CalculatorShould
    {
        [Test]
        public void Check_CalculateEmployeePay_method_returns_correct_pay()
        {
            // Arrange
            Mock<IRepository<PermEmployeeData>> perm = new Mock<IRepository<PermEmployeeData>>();
            Mock<IRepository<TempEmployeeData>> temp = new Mock<IRepository<TempEmployeeData>>();
            Calculator sut = new Calculator(perm.Object, temp.Object);

            perm.Setup(x => x.Read(It.IsAny<string>())).Returns(new PermEmployeeData()
            { 
                EmployeeID = "007",
                FName = "James",
                LName = "Bond",
                Salaryint = 23000,
                Bonusint = 2000
            });

            // Act
            var x = sut.CalculateEmployeePay(perm.Object.GetID("007"));

            // Assert
            Assert.That(x, Is.EqualTo(23708));
        }
    }
}