using dotenv.net;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

// Load environment variables and request a connection to PSQL DB
DotEnv.Load();
var envVars = DotEnv.Read();
string DatabaseUrl = envVars["DATABASE_URL"];

try{
    await using var conn = new NpgsqlConnection(DatabaseUrl);
} catch(Exception ex){
    Console.WriteLine($"lasdadssol: {ex.Message}");
};


app.MapGet("/", () => "Tessssdsdsssdsdsdsdsdsdasdaasdasdasdasdsddsdsdsdssdst");

app.Run();
