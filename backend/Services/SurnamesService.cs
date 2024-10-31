using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class SurnamesService
    {
        private readonly DataDbContext _context;

        public SurnamesService(DataDbContext context)
        {
            _context = context;
        }
    
        public async Task<List<Surname>> GetAllSurnamesAsync()
        {
            return await _context.Surnames.ToListAsync();
        }

        public async Task<Surname> GetRandomSingleSurname()
        {
            var totalCountOfSurnames = await _context.Surnames.CountAsync();
            if (totalCountOfSurnames == 0)
            {
                throw new InvalidOperationException("No surnames available in the database.");
            }
            var random = new Random();
            int randomId = random.Next(1, totalCountOfSurnames + 1);
            // fetch by random id 
            var surname = await _context.Surnames.FindAsync(randomId);
            if (surname == null)
            {
                throw new InvalidOperationException($"Surname with ID {randomId} not found.");
            }
            return surname;
        }
    }

}