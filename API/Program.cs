using Microsoft.EntityFrameworkCore;
using PayCal.DataAccess;
using PayCal.Models;
using PayCal.Repositories;
using PayCal.Repositories.Persistent;
using PayCal.Services;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

var builder = WebApplication.CreateBuilder(args);
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.ResolveConflictingActions (apiDescriptions => apiDescriptions.First());
});

builder.Services.AddDbContext<EmployeeContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<IRepository<PermEmployeeData>, PermEmployeeRepository>();
builder.Services.AddScoped<IRepository<TempEmployeeData>, TempEmployeeRepository>();
builder.Services.AddScoped<ICalculator, Calculator>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();