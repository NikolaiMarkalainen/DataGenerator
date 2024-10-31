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
    }
}