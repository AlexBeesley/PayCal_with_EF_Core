using System;
using System.Collections.Generic;

namespace PayCal
{
    public interface IRepository<T>
    {
        T Create(string fname, string lname, int? Salary_or_DayRate, int? Bonus_or_WeeksWorked);
        IEnumerable<T> ReadAll();
        int GetIDfromIndex(int employeeID);
        T Read(int employeeID);
        int Count();
        T Update(int employeeID, string fname, string lname, int? Salary_or_DayRate, int? Bonus_or_WeeksWorked);
        bool Delete(int employeeID);
    }
}
