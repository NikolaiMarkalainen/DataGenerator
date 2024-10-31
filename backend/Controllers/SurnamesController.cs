using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers
{
    [ApiController]
    [Route("/surnames")]
    public class SurnamesController : ControllerBase
    {
        private readonly DataDbContext _context;
        // constructor to add DB context to controller
        public SurnamesController(DataDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _context.Surnames.ToListAsync();
            return Ok(countries);
        }
    }
}