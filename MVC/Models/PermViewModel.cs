using PayCal.Models;

namespace PayCal_MVC.Models
{
    public class PermViewModel
    {
        public List<PermEmployeeData>? Employees { get; set; }
        public PermEmployeeData? Employee { get; set; }
        public PermEmployeeData? PayCalDetails { get; set; }
        public double? PayCalculated { get; set; }
        public PermEmployeeData? Created { get; set; }
        public PermEmployeeData? Updated { get; set; }
        public string? DeletedID { get; set; }
        public PermEmployeeData? Deleted { get; set; }
    }
}
