using PayCal.Models;
using PayCal.Logging;
using log4net;
using System.Reflection;

namespace PayCal.Repositories.Volatile
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
                EmployeeID = rnd.Next(1000,9999).ToString(),
                FName = "Joe",
                LName = "Bloggs",
                Salaryint = 40000,
                Bonusint = 5000
            },

            new PermEmployeeData()
            {
                EmployeeID = rnd.Next(1000,9999).ToString(),
                FName = "John",
                LName = "Smith",
                Salaryint = 45000,
                Bonusint = 2500
            },
            
            new PermEmployeeData()
            {
                EmployeeID = rnd.Next(1000,9999).ToString(),
                FName = "Harry",
                LName = "Potter",
                Salaryint = 23000,
                Bonusint = 2300
            },
            
            new PermEmployeeData()
            {
                EmployeeID = rnd.Next(1000,9999).ToString(),
                FName = "Jane",
                LName = "Doe",
                Salaryint = 48000,
                Bonusint = 5000
            },
            
            new PermEmployeeData()
            {
                EmployeeID = rnd.Next(1000,9999).ToString(),
                FName = "James",
                LName = "Bond",
                Salaryint = 67000,
                Bonusint = 12000
            }
        };

        public PermEmployeeData Create(string fname, string lname, int Salary, int Bonus)
        {
            var createPermEmployeeData = new PermEmployeeData()
            {
                EmployeeID = rnd.Next(1000, 9999).ToString(),
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

        public string GetID(string employeeID)
        {
            _log.Debug($"\nID from index: {myPermEmployeeData[Convert.ToInt32(employeeID)].EmployeeID}");
            return myPermEmployeeData[Convert.ToInt32(employeeID)].EmployeeID;
        }

        public PermEmployeeData Read(string employeeID)
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

        public PermEmployeeData Update(string employeeID, string fname, string lname, int Salary, int Bonus)
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

        public bool Delete(string employeeID)
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