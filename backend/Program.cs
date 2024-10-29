using dotenv.net;
using Npgsql;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables and request a connection to PSQL DB
DotEnv.Load();
var envVars = DotEnv.Read();
string databaseUrl = envVars["DATABASE_URL"];

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var surnameData = File.ReadAllText("./data/json_data/surnames.json");
var firstnameData = File.ReadAllText("./data/json_data/firstnames.json");
var countriesData = File.ReadAllText("./data/json_data/countries.json");
var wordsData = File.ReadAllText("./data/json_data/words.json");

var surnames = JsonConvert.DeserializeObject<List<string>>(surnameData);
var words = JsonConvert.DeserializeObject<List<string>>(wordsData);
var firstNames = JsonConvert.DeserializeObject<List<string>>(firstnameData);
var countries = JsonConvert.DeserializeObject<List<string>>(countriesData);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

try{
    await using var conn = new NpgsqlConnection(databaseUrl);
    await conn.OpenAsync();
    Console.WriteLine("Connected to database");
} catch(Exception ex){
    Console.WriteLine($"Error: {ex.Message}");
};


app.MapGet("/", () => "main");

app.Run();
