using Moq;
using NUnit.Framework;
using PayCal.Repositories;
using PayCal.Services;
using PayCal.Models;
using PayCal_API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Tests.APITests.ControllerTests
{
    public class TempControllerShould
    {
        private readonly Mock<IRepository<TempEmployeeData>> _repo = new Mock<IRepository<TempEmployeeData>>();
        private readonly Mock<ICalculator> _ICal = new Mock<ICalculator>();
        private readonly TempEmployeeController _sut;

        public TempControllerShould()
        {
            _sut = new TempEmployeeController(_repo.Object, _ICal.Object);
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
            var response = _sut.GetTempEmployees();
            var contentResponse = response as OkObjectResult;
            List<TempEmployeeData> responseAsList = contentResponse.Value as List<TempEmployeeData>;

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.AreEqual(responseAsList.Count, 2);
        }

        [Test]
        public void Return_Employee_When_GetTempEmployeeByID_Is_Called()
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
                        WeeksWorkedint = WeeksWorked
                    });

            // Act
            var response = _sut.GetTempEmployeeByID(ID) as OkObjectResult;
            var responseAsJSON = response.Value as JsonResult;

            // Assert
            Assert.IsInstanceOf<JsonResult>(response.Value);
            Assert.True(responseAsJSON.SerializerSettings.ToString().Contains(LastName));
        }

        [Test]
        public void Return_Updated_Employee_When_UpdateTempEmployee_Is_Called()
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
                        WeeksWorkedint = WeeksWorked
                    });

            _repo.Setup(repo => repo.Update(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new TempEmployeeData()
                {
                    EmployeeID = ID,
                    FName = FirstName,
                    LName = LastName,
                    DayRateint = DayRate,
                    WeeksWorkedint = WeeksWorked
                });
            
            // Act
            var response = _sut.UpdateTempEmployee(ID, null, "Smith", null, null);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(response);
        }

        [Test]
        public void Employee_is_deleted_When_DeleteTempEmployee_Is_Called()
        {
            // Arrange
            _repo.Setup(repo => repo.Delete(It.IsAny<string>()))
                .Returns(true);

            // Act
            var response = _sut.DeleteTempEmployee(ID);

            // Assert
            Assert.IsInstanceOf<OkResult>(response);
        }
    }
}
