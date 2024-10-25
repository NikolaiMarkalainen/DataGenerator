using dotenv.net;
using Npgsql;
using Microsoft.Extensions.Logging;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
logger.LogInformation($"lasdadssol: śadas");

app.UseHttpsRedirection();
logger.LogInformation($"lasdadssol: śadas");
// Load environment variables and request a connection to PSQL DB
DotEnv.Load();
var envVars = DotEnv.Read();
string DatabaseUrl = envVars["DATABASE_URL"];

try{
    await using var conn = new NpgsqlConnection(DatabaseUrl);
} catch(Exception ex){
    Console.WriteLine($"lasdadssol: {ex.Message}");
};


app.MapGet("/", () => "Tesssasdassddsdssdsssdsdsdsdsdsdasdaasdasdasdasdsddsdsdsdssdst");

app.Run();
