using PayCal.Models;
using PayCal.Logging;
using log4net;
using System.Reflection;
using PayCal.DataAccess;
using System.Text.Json;

namespace PayCal.Repositories.Persistent
{
    public class TempEmployeeRepository : IRepository<TempEmployeeData>
    {
        private readonly ILog _log;
        private readonly EmployeeContext _db;

        public TempEmployeeRepository(EmployeeContext db)
        {
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _db = db;
        }
        public TempEmployeeData Create(string fname, string lname, int DayRate, int WeeksWorked)
        {
            var createTempEmployeeData = new TempEmployeeData()
            {
                FName = fname,
                LName = lname,
                DayRateint = DayRate,
                WeeksWorkedint = WeeksWorked
            };
            _db.tempEmployeeDatas.Add(createTempEmployeeData);
            _db.SaveChanges();
            _log.Debug($"\nNew Employee created with ID: {createTempEmployeeData.EmployeeID}");
            return (createTempEmployeeData);
        }

        public IEnumerable<TempEmployeeData> ReadAll()
        {
            if (_db.tempEmployeeDatas.Count() == 0)
            {
                string path = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName, @"PayCal\", "TempMockData.json");
                string file = System.IO.File.ReadAllText(path);
                var employeeData = JsonSerializer.Deserialize<List<TempEmployeeData>>(file);
                _db.tempEmployeeDatas.AddRange(employeeData);
                _db.SaveChanges();
            }
            _log.Debug("\nReal All method accessed.");
            return _db.tempEmployeeDatas;
        }

        public string GetID(string fname)
        {
            var employeeData = _db.tempEmployeeDatas.First(e => e.FName == fname);
            _log.Debug($"\nID from fname: {employeeData.EmployeeID}");
            return employeeData.EmployeeID;
        }

        public TempEmployeeData Read(string employeeID)
        {
            if (_db.tempEmployeeDatas.Any(e => e.EmployeeID == employeeID))
            {
                TempEmployeeData employee = _db.tempEmployeeDatas.Find(employeeID);
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
            _log.Debug($"\nCount method accessed; count: {_db.tempEmployeeDatas.Count()}");
            return _db.tempEmployeeDatas.Count();
        }

        public TempEmployeeData Update(string employeeID, string fname, string lname, int DayRate, int WeeksWorked)
        {
            if (_db.tempEmployeeDatas.Any(e => e.EmployeeID == employeeID))
            {
                var x = Read(employeeID);
                x.FName = fname;
                x.LName = lname;
                x.DayRateint = DayRate;
                x.WeeksWorkedint = WeeksWorked;
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
            if (_db.tempEmployeeDatas.Any(e => e.EmployeeID == employeeID))
            {
                TempEmployeeData employee = _db.tempEmployeeDatas.FirstOrDefault(e => e.EmployeeID == employeeID);
                if (employee != null)
                {
                    _db.tempEmployeeDatas.Remove(employee);
                    _db.SaveChanges();
                    _log.Debug($"\nEmployee with ID: {employeeID} has been deleted.");
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