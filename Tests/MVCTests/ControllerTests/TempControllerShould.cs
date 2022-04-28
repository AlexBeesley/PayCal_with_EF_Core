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
    [TestFixture]
    public class TempControllerShould
    {
        private readonly Mock<IRepository<TempEmployeeData>> _repo = new Mock<IRepository<TempEmployeeData>>();
        private readonly Mock<ICalculator> _ICal = new Mock<ICalculator>();
        private readonly TempController _sut;

        public TempControllerShould()
        {
            _sut = new TempController(_repo.Object, _ICal.Object);
        }

        public string ID = "007";
        public string FirstName = "James";
        public string LastName = "Bond";
        public int DayRate = 100;
        public int WeeksWorked = 40;

        [Test]
        public void Return_Employees_When_Employees_Is_Called()
        {
            // Arrange
            _repo.Setup(repo => repo.ReadAll())
                .Returns(new List<TempEmployeeData>() { new TempEmployeeData(), new TempEmployeeData() });

            // Act
            var response = _sut.Employees();
            var count = ((TempViewModel)((ViewResult)response).Model).Employees.Count;

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
                    new TempEmployeeData()
                    {
                        EmployeeID = ID,
                        FName = FirstName,
                        LName = LastName,
                        DayRateint = DayRate,
                        WeeksWorkedint = WeeksWorked,
                    });

            // Act
            var response = _sut.Employee(ID);

            // Assert
            Assert.IsInstanceOf<ViewResult>(response);
            Assert.AreEqual(((TempViewModel)((ViewResult)response).Model).Employee.FName, "James");
        }

        [Test]
        public void Return_Employee_Tax_When_PayCal_Is_Called()
        {
            // Arrange
            _repo.Setup(repo => repo.Read(It.IsAny<string>()))
                .Returns(
                    new TempEmployeeData()
                    {
                        EmployeeID = ID,
                        FName = FirstName,
                        LName = LastName,
                        DayRateint = DayRate,
                        WeeksWorkedint = WeeksWorked,
                    });

            _ICal.Setup(cal => cal.CalculateEmployeePay(It.IsAny<string>()))
                .Returns(12345.67);

            // Act
            var response = _sut.PayCal(ID);

            // Assert
            Assert.IsInstanceOf<ViewResult>(response);
            Assert.AreEqual(((TempViewModel)((ViewResult)response).Model).PayCalculated, 12345.67);
        }

        [Test]
        public void Return_Edited_Employee_When_Edit_Is_Called()
        {
            // Arrange
            _repo.Setup(repo => repo.Read(It.IsAny<string>()))
                .Returns(
                    new TempEmployeeData()
                    {
                        EmployeeID = ID,
                        FName = FirstName,
                        LName = LastName,
                        DayRateint = DayRate,
                        WeeksWorkedint = WeeksWorked,
                    });

            _repo.Setup(repo => repo.Update(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(new TempEmployeeData()
                    {
                        EmployeeID = ID,
                        FName = FirstName,
                        LName = "Smith",
                        DayRateint = DayRate,
                        WeeksWorkedint = WeeksWorked,
                    });

            // Act
            var response = _sut.Edit(ID, null, "Smith", null, null);

            // Assert
            Assert.IsInstanceOf<ViewResult>(response);
            Assert.AreEqual(((TempViewModel)((ViewResult)response).Model).Updated.LName, "Smith");
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
