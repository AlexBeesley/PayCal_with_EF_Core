using PayCal.Models;

namespace PayCal_MVC.Models
{
    public class TempViewModel
    {
        public List<TempEmployeeData>? Employees { get; set; }
        public TempEmployeeData? Employee { get; set; }
        public TempEmployeeData? PayCalDetails { get; set; }
        public double? PayCalculated { get; set; }
        public TempEmployeeData? Created { get; set; }
        public TempEmployeeData? Updated { get; set; }
        public string? DeletedID { get; set; }
        public TempEmployeeData? Deleted { get; set; }
    }
}
