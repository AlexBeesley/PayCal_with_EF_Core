using Moq;
using NUnit.Framework;
using PayCal.Repositories;
using PayCal.Services;
using PayCal.Models;
using PayCal_MVC.Controllers;
using PayCal_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Tests.MVCTests.ControllerTests
{
    public class PermControllerShould
    {
        private readonly Mock<IRepository<PermEmployeeData>> _repo = new Mock<IRepository<PermEmployeeData>>();
        private readonly Mock<ICalculator> _ICal = new Mock<ICalculator>();
        private readonly PermController _sut;

        public PermControllerShould()
        {
            _sut = new PermController(_repo.Object, _ICal.Object);
        }

        public string ID = "007";
        public string FirstName = "James";
        public string LastName = "Bond";
        public int Salary = 2300;
        public int Bonus = 4000;

        [Test]
        public void Return_Employees_When_Employees_Is_Called()
        {
            // Arrange
            _repo.Setup(repo => repo.ReadAll())
                .Returns(new List<PermEmployeeData>() { new PermEmployeeData(), new PermEmployeeData() });

            // Act
            var response = _sut.Employees();
            var count = ((PermViewModel)((ViewResult)response).Model).Employees.Count;

            // Assert
            Assert.IsInstanceOf<ViewResult>(response);
            Assert.AreEqual(count, 2);
        }

        [Test]
        public void Return_Employee_When_Employee_Is_Called()
        {
            // Arrange
            _repo.Setup(repo => repo.Read(It.IsAny<string>()))
                .Returns(
                    new PermEmployeeData()
                    {
                        EmployeeID = ID,
                        FName = FirstName,
                        LName = LastName,
                        Salaryint = Salary,
                        Bonusint = Bonus
                    });

            // Act
            var response = _sut.Employee(ID);
            
            // Assert
            Assert.IsInstanceOf<ViewResult>(response);
            Assert.AreEqual(((PermViewModel)((ViewResult)response).Model).Employee.FName, "James");
        }

        [Test]
        public void Return_Employee_Tax_When_PayCal_Is_Called()
        {
            // Arrange
            _repo.Setup(repo => repo.Read(It.IsAny<string>()))
                .Returns(
                    new PermEmployeeData()
                    {
                        EmployeeID = ID,
                        FName = FirstName,
                        LName = LastName,
                        Salaryint = Salary,
                        Bonusint = Bonus
                    });

            _ICal.Setup(cal => cal.CalculateEmployeePay(It.IsAny<string>()))
                .Returns(12345.67);

            // Act
            var response = _sut.PayCal(ID);

            // Assert
            Assert.IsInstanceOf<ViewResult>(response);
            Assert.AreEqual(((PermViewModel)((ViewResult)response).Model).PayCalculated, 12345.67);
        }

        [Test]
        public void Return_Edited_Employee_When_Edit_Is_Called()
        {
            // Arrange
            _repo.Setup(repo => repo.Read(It.IsAny<string>()))
                .Returns(
                    new PermEmployeeData()
                    {
                        EmployeeID = ID,
                        FName = FirstName,
                        LName = LastName,
                        Salaryint = Salary,
                        Bonusint = Bonus
                    });
            
            _repo.Setup(repo => repo.Update(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(new PermEmployeeData()
                    {
                        EmployeeID = ID,
                        FName = FirstName,
                        LName = "Smith",
                        Salaryint = Salary,
                        Bonusint = Bonus
                    });
            
            // Act
            var response = _sut.Edit(ID, null, "Smith", null, null);

            // Assert
            Assert.IsInstanceOf<ViewResult>(response);
            Assert.AreEqual(((PermViewModel)((ViewResult)response).Model).Updated.LName, "Smith");
        }

        [Test]
        public void Employee_Is_Deleted_When_Delete_Is_Called()
        {
            // Arrange
            _repo.Setup(repo => repo.Delete(It.IsAny<string>()))
                .Returns(true);

            // Act
            var response = _sut.Delete(ID);

            // Assert
            Assert.IsInstanceOf<ViewResult>(response);
        }
    }
}
