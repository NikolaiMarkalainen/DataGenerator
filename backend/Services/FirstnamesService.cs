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
    }

}