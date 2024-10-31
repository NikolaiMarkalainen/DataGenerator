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
    }

}