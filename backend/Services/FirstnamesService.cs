using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class FirstnamesService
    {
        private readonly DataDbContext _context;

        public FirstnamesService(DataDbContext context)
        {
            _context = context;
        }
    
        public async Task<List<Firstname>> GetAllFirstnamesAsync()
        {
            return await _context.Firstnames.ToListAsync();
        }

        public async Task<Firstname> GetRandomFirstnameAsync()
        {
            var totalCountOfNames = await _context.Firstnames.CountAsync();
            if (totalCountOfNames == 0)
            {
                throw new InvalidOperationException("No surnames available in the database.");
            }
            var random = new Random();
            int randomId = random.Next(1, totalCountOfNames + 1);
            // fetch by random id 
            var firstname = await _context.Firstnames.FindAsync(randomId);
            if (firstname == null)
            {
                throw new InvalidOperationException($"Firstname with ID {randomId} not found.");
            }
            return firstname;
        }
    }

}