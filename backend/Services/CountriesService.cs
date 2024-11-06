using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class CountriesService
    {
        private readonly DataDbContext _context;

        public CountriesService(DataDbContext context)
        {
            _context = context;
        }

        public async Task<List<Country>> GetRandomCountriesAsync(int numberOfCountries = 1)
        {
            if(numberOfCountries <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfCountries), "Number of countries must be greater than zero.");
            }
            
            var totalCountOfCountries = await _context.Countries.CountAsync();
            
            if(totalCountOfCountries == 0)
            {
                throw new InvalidOperationException("No countries available.");
            }

            var availableCountries = Math.Min(numberOfCountries, totalCountOfCountries);
            var randomCountries = new List<Country>();

            var randomIds = new HashSet<int>();
            var random = new Random();

            while(randomIds.Count < availableCountries)
            {
                // add random ids to set
                int randomId = random.Next(1, totalCountOfCountries + 1);
                randomIds.Add(randomId);
            }

            foreach (var id in randomIds) 
            {
                var country = await _context.Countries.FindAsync(id);
                if (country != null)
                {
                    randomCountries.Add(country);
                }
            }
            return randomCountries;
        }
        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
        }
        public async Task<string> GetRandomCountryAsync()
        {
            var totalCountOfCountries = await _context.Countries.CountAsync();
            if(totalCountOfCountries == 0)
            {
                throw new InvalidOperationException("No countries available.");
            }
            var random = new Random();
            int randomIndex = random.Next(totalCountOfCountries);            
            var country = await _context.Countries.Skip(randomIndex).Take(1).FirstOrDefaultAsync();

            if(country != null)
            {
                return country.text;
            }
            throw new InvalidOperationException("Country not found.");
        }
        public async Task<Object> GenerateCountryDataAsync(CountryString countryString, int amount)
        {
            List<string> countryNames = new List<string>();
            Random random = new Random();
            if(countryString.AmountFixed == 1 && amount == 1 && countryString.Fixed)
            {
                return countryString.Text;
            }
            if(countryString.Fixed == false && amount == 1)
            {
                string country = await GetRandomCountryAsync();
                return country;
            }
            if (countryString.Fixed && countryString.AmountFixed == 1)
            {
                for (int i = 0; i < amount; i++)
                {
                    countryNames.Add(countryString.Text);
                }
                return countryNames;
            }
            if (countryString.Fixed && countryString.AmountFixed > 1)
            {
                List<Country> randomCountries = await GetRandomCountriesAsync(countryString.AmountFixed);

                for (int i = 0; i < amount; i++)
                {
                    int randomIndex = random.Next(randomCountries.Count);
                    countryNames.Add(randomCountries[randomIndex].text);
                }
            }
            if (countryString.Fixed == false)
            {
                for (int i = 0; i < amount; i++)
                {
                    string country = await GetRandomCountryAsync();
                    countryNames.Add(country);
                }
                
            }
            return countryNames;
        }
    }
}