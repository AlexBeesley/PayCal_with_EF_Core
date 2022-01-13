using PayCal.Models;
using PayCal.Repositories;
using PayCal.Services;

var builder = WebApplication.CreateBuilder(args);
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.ResolveConflictingActions (apiDescriptions => apiDescriptions.First());
});

builder.Services.AddSingleton<IRepository<PermEmployeeData>, PermEmployeeRepository>();
builder.Services.AddSingleton<IRepository<TempEmployeeData>, TempEmployeeRepository>();
builder.Services.AddSingleton<Calculator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            // Replace path with one relavent to your machine in order to get the front end wwwroot site working.
            builder.WithOrigins("file:///C:/Users/beesleyd/source/repos/PayCal%20API/wwwroot/index.html");
        });
});

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