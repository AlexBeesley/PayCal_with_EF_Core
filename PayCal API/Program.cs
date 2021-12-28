using PayCal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.ResolveConflictingActions (apiDescriptions => apiDescriptions.First());
});

builder.Services.AddSingleton<IRepository<PermEmployeeData>, PermEmployeeRepository>();
builder.Services.AddSingleton<IRepository<TempEmployeeData>, TempEmployeeRepository>();
builder.Services.AddSingleton<Calculator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
