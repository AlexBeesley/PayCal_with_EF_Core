using PayCal;

namespace PayCal_API.Services
{
    public class InjectService
    {
        private PermEmployeeRepository perm;
        private TempEmployeeRepository temp;

        public InjectService(IRepository<PermEmployeeData> Perm, IRepository<TempEmployeeData> Temp)
        {
            perm = (PermEmployeeRepository)Perm;
            temp = (TempEmployeeRepository)Temp;
        }

        public bool InjectNewEmployee(bool isperm, string fname, string lname, int salary_or_dayrate, int bonus_or_weeksworked)
        {
            try
            {
                if (isperm)
                {
                    perm.Create(fname, lname, salary_or_dayrate, bonus_or_weeksworked);
                }
                if (!isperm)
                {
                    temp.Create(fname, lname, salary_or_dayrate, bonus_or_weeksworked);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
