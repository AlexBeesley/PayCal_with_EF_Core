using System;
using System.Collections.Generic;
using System.Linq;
using PayCal.Models;

namespace PayCal.Repositories
{
    public class PermEmployeeRepository : IRepository<PermEmployeeData>
    {
        static Random rnd = new Random();
        private List<PermEmployeeData> myPermEmployeeData = new List<PermEmployeeData>()
        {
            new PermEmployeeData()
            {
                EmployeeID = rnd.Next(1000,9999),
                FName = "Joe",
                LName = "Bloggs",
                Salaryint = 40000,
                Bonusint = 5000
            },

            new PermEmployeeData()
            {
                EmployeeID = rnd.Next(1000,9999),
                FName = "John",
                LName = "Smith",
                Salaryint = 45000,
                Bonusint = 2500
            },
        };

        public PermEmployeeData Create(string fname, string lname, int? Salary, int? Bonus)
        {
            var createPermEmployeeData = new PermEmployeeData()
            {
                EmployeeID = rnd.Next(1000, 9999),
                FName = fname,
                LName = lname,
                Salaryint = Salary,
                Bonusint = Bonus,
            };
            myPermEmployeeData.Add(createPermEmployeeData);
            return createPermEmployeeData;
        }

        public IEnumerable<PermEmployeeData> ReadAll()
        {
            return (myPermEmployeeData);
        }

        public int GetIDfromIndex(int employeeID)
        {
            return myPermEmployeeData[employeeID].EmployeeID;
        }

        public PermEmployeeData Read(int employeeID)
        {
            if (myPermEmployeeData.Any(e => e.EmployeeID == employeeID))
            {
                PermEmployeeData employee = myPermEmployeeData.First(e => e.EmployeeID == employeeID);
                return employee;
            }
            else { return null; }
        }

        public int Count()
        {
            return myPermEmployeeData.Count;
        }

        public PermEmployeeData Update(int employeeID, string fname, string lname, int? Salary, int? Bonus)
        {
            if (myPermEmployeeData.Any(e => e.EmployeeID == employeeID))
            {
                var x = Read(employeeID);
                x.FName = fname;
                x.LName = lname;
                x.Salaryint = Salary;
                x.Bonusint = Bonus;
                return x;
            }
            else { return null; }
        }

        public bool Delete(int employeeID)
        {
            if (myPermEmployeeData.Any(e => e.EmployeeID == employeeID))
            {
                PermEmployeeData employee = myPermEmployeeData.FirstOrDefault(e => e.EmployeeID == employeeID);
                if (myPermEmployeeData.Remove(employee)) { return true; }
                else { return false; }
            }
            else { return false; }
        }
    }
}