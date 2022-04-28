﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PayCal.DataAccess;

#nullable disable

namespace PayCal.Migrations
{
    [DbContext(typeof(EmployeeContext))]
    partial class EmployeeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PayCal.Models.PermEmployeeData", b =>
                {
                    b.Property<string>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Bonusint")
                        .HasColumnType("int");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salaryint")
                        .HasColumnType("int");

                    b.HasKey("EmployeeID");

                    b.ToTable("permEmployeeDatas");
                });

            modelBuilder.Entity("PayCal.Models.TempEmployeeData", b =>
                {
                    b.Property<string>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("DayRateint")
                        .HasColumnType("int");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WeeksWorkedint")
                        .HasColumnType("int");

                    b.HasKey("EmployeeID");

                    b.ToTable("tempEmployeeDatas");
                });
#pragma warning restore 612, 618
        }
    }
}