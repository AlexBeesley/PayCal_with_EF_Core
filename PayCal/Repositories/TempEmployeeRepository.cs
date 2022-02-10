using PayCal.Models;
using PayCal.Logging;
using log4net;
using System.Reflection;

namespace PayCal.Repositories
{
    public class TempEmployeeRepository : IRepository<TempEmployeeData>
    {
        private readonly ILog _log;

        public TempEmployeeRepository()
        {
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

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
            },
            
            new TempEmployeeData()
            {
                EmployeeID = rnd.Next(1000,9999),
                FName = "Kate",
                LName = "Rugby",
                DayRateint = 240,
                WeeksWorkedint = 49
            },
            
            new TempEmployeeData()
            {
                EmployeeID = rnd.Next(1000,9999),
                FName = "Hassan",
                LName = "Ahmad",
                DayRateint = 120,
                WeeksWorkedint = 16
            },
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
            _log.Debug($"\nNew Employee created with ID: {createTempEmployeeData.EmployeeID}");
            return (createTempEmployeeData);
        }

        public IEnumerable<TempEmployeeData> ReadAll()
        {
            _log.Debug("\nReal All method accessed.");
            return myTempEmployeeData;
        }

        public int GetIDfromIndex(int employeeID)
        {
            _log.Debug($"\nID from index: {myTempEmployeeData[employeeID].EmployeeID}");
            return myTempEmployeeData[employeeID].EmployeeID;
        }

        public TempEmployeeData Read(int employeeID)
        {
            if (myTempEmployeeData.Any(e => e.EmployeeID == employeeID))
            {
                TempEmployeeData employee = myTempEmployeeData.First(e => e.EmployeeID == employeeID);
                _log.Debug($"\nRead method accessed for Employee with ID: {employee.EmployeeID}");
                return employee;
            }
            else {
                _log.Debug($"\n{LogStrings.ID_NotFound}{employeeID}");
                return null;
            }
        }

        public int Count()
        {
            _log.Debug($"\nCount method accessed; count: {myTempEmployeeData.Count}");
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
                _log.Debug($"\nEmployee with ID: {employeeID} has been updated.");
                return x;
            }
            else {
                _log.Debug($"\n{LogStrings.ID_NotFound}{employeeID}");
                return null;
            }
        }

        public bool Delete(int employeeID)
        {
            if (myTempEmployeeData.Any(e => e.EmployeeID == employeeID))
            {
                TempEmployeeData employee = myTempEmployeeData.FirstOrDefault(e => e.EmployeeID == employeeID);
                if (myTempEmployeeData.Remove(employee)) {
                    _log.Debug($"\nEmployee with ID: {employeeID} has been deleted.");
                    return true;
                }
                else {
                    _log.Debug($"\nFailed to delete Employee with ID: {employeeID}");
                    return false;
                }
            }
            else {
                _log.Debug($"\n{LogStrings.ID_NotFound}{employeeID}");
                return false;
            }
        }
    }
}