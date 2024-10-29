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
        SeedData(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
        private void SeedData(ModelBuilder modelBuilder)
    {
        var firstnameData = File.ReadAllText("./data/json_data/firstname_keys.json");
        var firstnames = JsonConvert.DeserializeObject<List<GenericItem>>(firstnameData);
        modelBuilder.Entity<Firstname>().HasData(firstnames);

        var surnameData = File.ReadAllText("./data/json_data/surname_keys.json");
        var surnames = JsonConvert.DeserializeObject<List<GenericItem>>(surnameData);
        modelBuilder.Entity<Surname>().HasData(surnames);

        var wordsData = File.ReadAllText("./data/json_data/words_keys.json");
        var words = JsonConvert.DeserializeObject<List<GenericItem>>(wordsData);
        modelBuilder.Entity<Word>().HasData(words);

        var countriesData = File.ReadAllText("./data/json_data/countries.json");
        var countries = JsonConvert.DeserializeObject<List<Countries>>(countriesData);
        modelBuilder.Entity<Countries>().HasData(countries);
    }
}


public class Firstname
{
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