using PayCal.Models;
using PayCal.Repositories;

namespace PayCal_MVC.Models {
    public class HomeViewModel
    {
        public int? permCount { get; set; }
        public int? tempCount { get; set; }
        public List<TempEmployeeData>? tempList { get; set;}
        public List<PermEmployeeData>? permList { get; set; }
    }
}
