using Microsoft.EntityFrameworkCore;
using PayCal.DataAccess;
using PayCal.Models;
using PayCal.Repositories;
using PayCal.Repositories.Persistent;
using PayCal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EmployeeContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<IRepository<PermEmployeeData>, PermEmployeeRepository>();
builder.Services.AddScoped<IRepository<TempEmployeeData>, TempEmployeeRepository>();
builder.Services.AddScoped<ICalculator, Calculator>();

var app = builder.Build();

app.UseStatusCodePagesWithRedirects("/Error/{0}");
app.UseExceptionHandler("/Home/Error");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
