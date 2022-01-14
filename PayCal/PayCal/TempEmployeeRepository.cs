using System;
using System.Collections.Generic;
using System.Linq;
using PayCal.Models;

namespace PayCal.Repositories
{
    public class TempEmployeeRepository : IRepository<TempEmployeeData>
    {
        static Random rnd = new Random();
        private List<TempEmployeeData> myTempEmployeeData = new List<TempEmployeeData>()
        {
            new TempEmployeeData()
            {
                EmployeeID = rnd.Next(1000,9999),
                FName = "Clare",
                LName = "Jones",
                DayRateint = 350,
                WeeksWorkedint = 40
            }
        };

        public TempEmployeeData Create(string fname, string lname, int? DayRate, int? WeeksWorked)
        {
            var createTempEmployeeData = new TempEmployeeData()
            {
                EmployeeID = rnd.Next(1000, 9999),
                FName = fname,
                LName = lname,
                DayRateint = DayRate,
                WeeksWorkedint = WeeksWorked
            };
            myTempEmployeeData.Add(createTempEmployeeData);
            return (createTempEmployeeData);
        }

        public IEnumerable<TempEmployeeData> ReadAll()
        {
            return myTempEmployeeData;
        }

        public int GetIDfromIndex(int employeeID)
        {
            return myTempEmployeeData[employeeID].EmployeeID;
        }

        public TempEmployeeData Read(int employeeID)
        {
            if (myTempEmployeeData.Any(e => e.EmployeeID == employeeID)) {
                TempEmployeeData employee = myTempEmployeeData.First(e => e.EmployeeID == employeeID);
                return employee;
            }
            else { return null; }
        }

        public int Count()
        {
            return myTempEmployeeData.Count;
        }

        public TempEmployeeData Update(int employeeID, string fname, string lname, int? DayRate, int? WeeksWorked)
        {
            if (myTempEmployeeData.Any(e => e.EmployeeID == employeeID))
            {
                var x = Read(employeeID);
                x.FName = fname;
                x.LName = lname;
                x.DayRateint = DayRate;
                x.WeeksWorkedint = WeeksWorked;
                return x;
            }
            else { return null; }
        }

        public bool Delete(int employeeID)
        {
            if (myTempEmployeeData.Any(e => e.EmployeeID == employeeID))
            {
                TempEmployeeData employee = myTempEmployeeData.FirstOrDefault(e => e.EmployeeID == employeeID);
                if (myTempEmployeeData.Remove(employee)) { return true; }
                else { return false; }
            }
            else { return false; }
        }
    }
}

