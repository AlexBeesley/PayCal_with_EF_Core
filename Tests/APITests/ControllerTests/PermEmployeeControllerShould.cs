using Moq;
using NUnit.Framework;
using PayCal.Repositories;
using PayCal.Services;
using PayCal.Models;
using PayCal_API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Tests.APITests.ControllerTests
{
    public class PermControllerShould
    {
        private readonly Mock<IRepository<PermEmployeeData>> _repo = new Mock<IRepository<PermEmployeeData>>();
        private readonly Mock<ICalculator> _ICal = new Mock<ICalculator>();
        private readonly PermEmployeeController _sut;

        public PermControllerShould()
        {
            _sut = new PermEmployeeController(_repo.Object, _ICal.Object);
        }

        public string ID = "007";
        public string FirstName = "James";
        public string LastName = "Bond";
        public int Salary = 23000;
        public int Bonus = 4000;

        [Test]
        public void Return_Employees_When_Employees_Is_Called()
        {
            // Arrange
            _repo.Setup(repo => repo.ReadAll())
                .Returns(new List<PermEmployeeData>() { new PermEmployeeData(), new PermEmployeeData() });

            // Act
            var response = _sut.GetAllPermEmployees();
            var contentResponse = response as OkObjectResult;
            List<PermEmployeeData> responseAsList = contentResponse.Value as List<PermEmployeeData>;

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.AreEqual(responseAsList.Count, 2);
        }

        [Test]
        public void Return_Employee_When_GetPermEmployeeByID_Is_Called()
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
            var response = _sut.GetPermEmployeeByID(ID) as OkObjectResult;
            var responseAsJSON = response.Value as JsonResult;

            // Assert
            Assert.IsInstanceOf<JsonResult>(response.Value);
            Assert.True(responseAsJSON.SerializerSettings.ToString().Contains(LastName));
        }
        
        [Test]
        public void Return_Updated_Employee_When_UpdatePermEmployee_Is_Called()
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
            var response = _sut.UpdatePermEmployee(ID, null, "Smith", null, null);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(response);
        }

        [Test]
        public void Employee_is_deleted_When_DeletePermEmployee_Is_Called()
        {
            // Arrange
            _repo.Setup(repo => repo.Delete(It.IsAny<string>()))
                .Returns(true);

            // Act
            var response = _sut.DeletePermEmployee(ID);

            // Assert
            Assert.IsInstanceOf<OkResult>(response);
        }
    }
}
