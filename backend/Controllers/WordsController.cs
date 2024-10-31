using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers
{
    [ApiController]
    [Route("/words")]
    public class WordsController : ControllerBase
    {
        private readonly DataDbContext _context;
        // constructor to add DB context to controller
        public WordsController(DataDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _context.Words.ToListAsync();
            return Ok(countries);
        }
    }
}