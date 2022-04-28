using Microsoft.EntityFrameworkCore;
using PayCal.Models;

namespace PayCal.DataAccess
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext() { }

        private const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PayCalDB;Integrated Security=False;";

        public EmployeeContext(DbContextOptions options) : base(options) { }
        public DbSet<TempEmployeeData>? tempEmployeeDatas { get; set; }
        public DbSet<PermEmployeeData>? permEmployeeDatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}