using PayCal;

namespace PayCal_API.Services
{
    public class ReturnService
    {
        private PermEmployeeRepository perm;
        private TempEmployeeRepository temp;

        public ReturnService(IRepository<PermEmployeeData> Perm, IRepository<TempEmployeeData> Temp)
        {
            perm = (PermEmployeeRepository)Perm;
            temp = (TempEmployeeRepository)Temp;
        }

        public string ReturnAllEmployees()
        {
            return ($"{(string.Concat(perm.ReadAll()))}{string.Concat(temp.ReadAll())}");
        }

        public string ReturnSingleEmployee(int ID)
        {
            try
            {
                //return (perm.Read(ID).ToString());
                return (string.Concat(perm.Read(ID)));
            }
            catch
            {
                return (temp.Read(ID).ToString());
            }
        }

        public int ReturnSingleEmployeeID(int ID)
        {
            try
            {
                return (perm.Read(ID).EmployeeID);
            }
            catch
            {
                return (temp.Read(ID).EmployeeID);
            }
        }

        public string ReturnEmploymentType(int ID)
        {
            try
            {
                return ($"Employee with ID: {perm.Read(ID).EmployeeID} is Permanent");
            }
            catch
            {
                return ($"Employee with ID: {temp.Read(ID).EmployeeID} is Temporary");
            }
        }

        public string ReturnFullName(int ID)
        {
            try
            {
                return ($"{perm.Read(ID).FName} {perm.Read(ID).LName}");
            }
            catch
            {
                return ($"{temp.Read(ID).FName} {temp.Read(ID).LName}");
            }
        }

        public int? ReturnSalary(int ID)
        {
            return (perm.Read(ID).Salaryint);
        }

        public int? ReturnBonus(int ID)
        {
            return (perm.Read(ID).Bonusint);
        }

        public int? ReturnDayRate(int ID)
        {
            return (temp.Read(ID).DayRateint);
        }

        public int? ReturnWeeksWorked(int ID)
        {
            return (temp.Read(ID).WeeksWorkedint);
        }
    }
}