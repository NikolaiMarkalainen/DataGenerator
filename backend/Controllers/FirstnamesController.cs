using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers
{
    [ApiController]
    [Route("/firstnames")]
    public class FirstnamesController : ControllerBase
    {
        private readonly DataDbContext _context;
        // constructor to add DB context to controller
        public FirstnamesController(DataDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _context.Firstnames.ToListAsync();
            return Ok(countries);
        }
    }
}