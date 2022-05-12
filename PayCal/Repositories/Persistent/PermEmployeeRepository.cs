using PayCal.Models;
using PayCal.Logging;
using log4net;
using System.Reflection;
using PayCal.DataAccess;
using System.Text.Json;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace PayCal.Repositories.Persistent
{
    public class PermEmployeeRepository : IRepository<PermEmployeeData>
    {
        private readonly ILog _log;
        private readonly EmployeeContext _db;

        public PermEmployeeRepository(EmployeeContext db)
        {
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _db = db;
        }

        public PermEmployeeData Create(string fname, string lname, int Salary, int Bonus)
        {
            var createPermEmployeeData = new PermEmployeeData()
            {
                FName = fname,
                LName = lname,
                Salaryint = Salary,
                Bonusint = Bonus,
            };
            _db.permEmployeeDatas.Add(createPermEmployeeData);
            _db.SaveChanges();
            _log.Debug($"\nNew Employee created with ID: {createPermEmployeeData.EmployeeID}");
            return createPermEmployeeData;
        }

        public IEnumerable<PermEmployeeData> ReadAll()
        {
            if (_db.permEmployeeDatas.Count() == 0)
            {
                string path = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName, @"PayCal\", "PermMockData.json");
                string file = System.IO.File.ReadAllText(path);
                var employeeData = JsonSerializer.Deserialize<List<PermEmployeeData>>(file);
                _db.permEmployeeDatas.AddRange(employeeData);
                _db.SaveChanges();
            }
            _log.Debug("\nReal All method accessed.");
            return _db.permEmployeeDatas;
        }

        public string GetID(string fname)
        {
            var employeeData = _db.permEmployeeDatas.FirstOrDefault(e => e.FName == fname);
            _log.Debug($"\nID from fname: {employeeData.EmployeeID}");
            return employeeData.EmployeeID;
        }

        public PermEmployeeData Read(string employeeID)
        {
            if (_db.permEmployeeDatas.Any(e => e.EmployeeID == employeeID))
            {
                PermEmployeeData employee = _db.permEmployeeDatas.Find(employeeID);
                return employee;
            }
            else
            {
                _log.Debug($"\n{LogStrings.ID_NotFound}{employeeID}");
                return null;
            }
        }

        public int Count()
        {
            _log.Debug($"\nCount method accessed; count: {_db.permEmployeeDatas.Count()}");
            return _db.permEmployeeDatas.Count();
        }

        public PermEmployeeData Update(string employeeID, string fname, string lname, int Salary, int Bonus)
        {
            if (_db.permEmployeeDatas.Any(e => e.EmployeeID == employeeID))
            {
                var x = Read(employeeID);
                x.FName = fname;
                x.LName = lname;
                x.Salaryint = Salary;
                x.Bonusint = Bonus;
                _log.Debug($"\nEmployee with ID: {employeeID} has been updated.");
                _db.SaveChanges();
                return x;
            }
            else
            {
                _log.Debug($"\n{LogStrings.ID_NotFound}{employeeID}");
                return null;
            }
        }

        public bool Delete(string employeeID)
        {
            if (_db.permEmployeeDatas.Any(e => e.EmployeeID == employeeID))
            {
                PermEmployeeData? employee = _db.permEmployeeDatas.FirstOrDefault(e => e.EmployeeID == employeeID);
                if (employee != null)
                {
                    _db.permEmployeeDatas.Remove(employee);
                    _log.Debug($"\nEmployee with ID: {employeeID} has been deleted.");
                    _db.SaveChanges();
                    return true;
                }
                else
                {
                    _log.Debug($"\nFailed to delete Employee with ID: {employeeID}");
                    return false;
                }
            }
            else
            {
                _log.Debug($"\n{LogStrings.ID_NotFound}{employeeID}");
                return false;
            }
        }
    }
}