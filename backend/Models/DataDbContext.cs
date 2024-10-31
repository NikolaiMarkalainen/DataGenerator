using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class DataDbContext: DbContext
{
    public DataDbContext(DbContextOptions<DataDbContext> options): base(options) {}

    public DbSet<Countries> Countries { get; set; }
    public DbSet<Firstname> Firstnames{ get; set; }
    public DbSet<Surname> Surnames { get; set; }
    public DbSet<Word> Words { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    public async Task SeedDatabaseAsync()
    {
        if (!await Firstnames.AnyAsync())
        {
            var firstnameData = File.ReadAllText("./data/json_data/firstname_keys.json");
            var firstnames = JsonConvert.DeserializeObject<List<Firstname>>(firstnameData);
            await Firstnames.AddRangeAsync(firstnames);
        }

        if (!await Surnames.AnyAsync())
        {
            var surnameData = File.ReadAllText("./data/json_data/surname_keys.json");
            var surnames = JsonConvert.DeserializeObject<List<Surname>>(surnameData);
            await Surnames.AddRangeAsync(surnames);
        }

        if (!await Words.AnyAsync())
        {
            var wordsData = File.ReadAllText("./data/json_data/words_keys.json");
            var words = JsonConvert.DeserializeObject<List<Word>>(wordsData);
            await Words.AddRangeAsync(words);
        }

        if (!await Countries.AnyAsync())
        {
            var countriesData = File.ReadAllText("./data/json_data/countries_keys.json");
            var countries = JsonConvert.DeserializeObject<List<Countries>>(countriesData);
            await Countries.AddRangeAsync(countries);
        }

        await SaveChangesAsync();
    }
}


public class Firstname
{
    public int Id { get; set; }
    public required string text { get; set; }
};

public class Countries
{
    public int Id { get; set; }
    public required string text { get; set; }
    public required string key { get; set; }
};


public class Surname
{
    public int Id { get; set; }
    public required string text { get; set; }
};


public class Word
{
     public int Id { get; set; }
    public required string text { get; set; }
};

public class GenericItem
{
    public required string text { get; set; }
};