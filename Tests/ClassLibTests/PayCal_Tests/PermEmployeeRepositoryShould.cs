﻿using NUnit.Framework;
using PayCal.Repositories.Volatile;

namespace PayCal_Tests
{
    [TestFixture]
    public class PermEmployeeRepositoryShould
    {
        [Test]
        public void Check_Create_method_adds_data_to_list()
        {
            // Arrange
            var sut = new PermEmployeeRepository();

            // Act
            string? x = sut.Create("Alex", "Beesley", 23000, 3000).EmployeeID;

            // Assert
            Assert.That(sut.Read(x).FName, Is.EqualTo("Alex"));
        }

        [Test]
        public void Check_ReadAll_method_returns_all()
        {
            // Arrange
            var sut = new PermEmployeeRepository();

            // Act
            var x = sut.ReadAll();

            // Assert
            Assert.That(string.Concat(x).Split("ID").Length, Is.EqualTo(6));
        }

        [Test]
        public void Check_Read_method_returns_all_values_in_list()
        {
            // Arrange
            var sut = new PermEmployeeRepository();

            // Act
            var x = sut.Read(sut.GetID("0"));

            // Assert
            Assert.That(x.FName, Is.EqualTo("Joe"));
        }

        [Test]
        public void Check_Delete_method_returns_true()
        {
            // Arrange
            var sut = new PermEmployeeRepository();
            string employeeID = sut.GetID("1");

            // Act
            var x = sut.Delete(employeeID);

            // Assert
            Assert.True(x);
        }
    }
}
