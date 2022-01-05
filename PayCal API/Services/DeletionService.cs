using PayCal;

namespace PayCal_API.Services
{
    public class DeletionService
    {
        private PermEmployeeRepository perm;
        private TempEmployeeRepository temp;

        public DeletionService(IRepository<PermEmployeeData> Perm, IRepository<TempEmployeeData> Temp)
        {
            perm = (PermEmployeeRepository)Perm;
            temp = (TempEmployeeRepository)Temp;
        }

        public bool DeleteEmployee(int ID)
        {
            try
            {
                perm.Delete(ID);
                return true;
            }
            catch
            {
                temp.Delete(ID);
                return false;
            }
        }
    }
}
