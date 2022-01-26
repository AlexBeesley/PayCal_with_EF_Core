using PayCal.Models;
using PayCal.Logging;
using log4net;
using System.Reflection;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace PayCal.Repositories
{
    public class PermEmployeeRepository : IRepository<PermEmployeeData>
    {
        private readonly ILog _log;

        public PermEmployeeRepository()
        {
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

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
            _log.Debug($"\nNew Employee created with ID: {createPermEmployeeData.EmployeeID}");
            return createPermEmployeeData;
        }

        public IEnumerable<PermEmployeeData> ReadAll()
        {
            _log.Debug("\nReal All method accessed.");
            return (myPermEmployeeData);
        }

        public int GetIDfromIndex(int employeeID)
        {
            _log.Debug($"\nID from index: {myPermEmployeeData[employeeID].EmployeeID}");
            return myPermEmployeeData[employeeID].EmployeeID;
        }

        public PermEmployeeData Read(int employeeID)
        {
            if (myPermEmployeeData.Any(e => e.EmployeeID == employeeID))
            {
                PermEmployeeData employee = myPermEmployeeData.First(e => e.EmployeeID == employeeID);
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
            _log.Debug($"\nCount method accessed; count: {myPermEmployeeData.Count}");
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
            if (myPermEmployeeData.Any(e => e.EmployeeID == employeeID))
            {
                PermEmployeeData employee = myPermEmployeeData.FirstOrDefault(e => e.EmployeeID == employeeID);
                if (myPermEmployeeData.Remove(employee)) {
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