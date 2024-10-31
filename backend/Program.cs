using dotenv.net;
using Microsoft.EntityFrameworkCore;
using backend.Controllers;
using backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables and request a connection to PSQL DB
DotEnv.Load();
var envVars = DotEnv.Read();
string databaseUrl = envVars["DATABASE_URL"];

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//api and service loading
builder.Services.AddControllers();
builder.Services.AddScoped<CountriesService>();

builder.Services.AddDbContext<DataDbContext>(options =>
    options.UseNpgsql(databaseUrl));

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();
builder.Logging.AddConsole();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
// Apply migrations and seed data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataDbContext>();

    try
    {
        await dbContext.Database.MigrateAsync();
        await dbContext.SeedDatabaseAsync();
        logger.LogInformation("Database seeding completed successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError($"An error occurred during migration or seeding: {ex.Message}");
    }
}


app.MapGet("/", () => "main");

app.Run();
