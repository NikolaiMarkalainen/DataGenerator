using dotenv.net;
using Microsoft.EntityFrameworkCore;
using backend.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Load environment variables and request a connection to PSQL DB
DotEnv.Load();
var envVars = DotEnv.Read();
string databaseUrl = envVars["DATABASE_URL"];

builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policyBuilder => {
                policyBuilder.WithOrigins("http://localhost:3000");
                policyBuilder.AllowAnyHeader();
                policyBuilder.AllowAnyMethod();
            });
        });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//api and service loading
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.Converters.Add(new VariableDataConverter());
    });

builder.Services.AddScoped<CountriesService>();
builder.Services.AddScoped<FirstnamesService>();
builder.Services.AddScoped<SurnamesService>();
builder.Services.AddScoped<WordsService>();
builder.Services.AddScoped<GenerateRandomService>();
builder.Services.AddScoped<FileService>();
builder.Services.AddDbContext<DataDbContext>(options =>
    options.UseNpgsql(databaseUrl));

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json" , optional: true, reloadOnChange: false)
    .AddEnvironmentVariables()
    .Build();

var app = builder.Build();

app.UseCors("CorsPolicy");

var logger = app.Services.GetRequiredService<ILogger<Program>>();
builder.Logging.AddConsole();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
