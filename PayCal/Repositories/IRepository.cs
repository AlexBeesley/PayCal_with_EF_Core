using System;
using System.Collections.Generic;

namespace PayCal.Repositories
{
    public interface IRepository<T>
    {
        T Create(string fname, string lname, int Salary_or_DayRate, int Bonus_or_WeeksWorked);
        IEnumerable<T> ReadAll();
        string GetID(string fname_or_index);
        T Read(string employeeID);
        int Count();
        T Update(string employeeID, string fname, string lname, int Salary_or_DayRate, int Bonus_or_WeeksWorked);
        bool Delete(string employeeID);
    }
}
