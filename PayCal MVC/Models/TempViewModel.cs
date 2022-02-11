using PayCal.Models;

namespace PayCal_MVC.Models
{
    public class TempViewModel
    {
        public string? Employees { get; set; }
        public string? Employee { get; set; }
        public EmployeeData? PayCalDetails { get; set; }
        public double? PayCalculated { get; set; }
        public EmployeeData? Created { get; set; }
        public EmployeeData? Updated { get; set; }
        public int? DeletedID { get; set; }
        public EmployeeData? Deleted { get; set; }
    }
}
