using PayCal.Models;
using PayCal.Repositories;
using PayCal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IRepository<PermEmployeeData>, PermEmployeeRepository>();
builder.Services.AddSingleton<IRepository<TempEmployeeData>, TempEmployeeRepository>();
builder.Services.AddSingleton<ICalculator, Calculator>();

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
