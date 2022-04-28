using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayCal.Migrations
{
    public partial class InitialDBCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "permEmployeeDatas",
                columns: table => new
                {
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Salaryint = table.Column<int>(type: "int", nullable: false),
                    Bonusint = table.Column<int>(type: "int", nullable: false),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permEmployeeDatas", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "tempEmployeeDatas",
                columns: table => new
                {
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DayRateint = table.Column<int>(type: "int", nullable: false),
                    WeeksWorkedint = table.Column<int>(type: "int", nullable: false),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tempEmployeeDatas", x => x.EmployeeID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "permEmployeeDatas");

            migrationBuilder.DropTable(
                name: "tempEmployeeDatas");
        }
    }
}
