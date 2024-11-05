using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class WordsService
    {
        private readonly DataDbContext _context;

        public WordsService(DataDbContext context)
        {
            _context = context;
        }
    
        public async Task<List<Word>> GetAllWordsAsync()
        {
            return await _context.Words.ToListAsync();
        }
        public async Task<string> GetRandomWordAsync()
        {
            var totalCountOfWords = await _context.Words.CountAsync();
            if (totalCountOfWords == 0)
            {
                throw new InvalidOperationException("No surnames available in the database.");
            }
            var random = new Random();
            int randomId = random.Next(1, totalCountOfWords + 1);
            // fetch by random id 
            var word = await _context.Words.FindAsync(randomId);
            if (word == null)
            {
                throw new InvalidOperationException($"Surname with ID {randomId} not found.");
            }
            return word.text;
        }
    }

}