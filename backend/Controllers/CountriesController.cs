using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers
{
    [ApiController]
    [Route("/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly DataDbContext _context;
        // constructor to add DB context to controller
        public CountriesController(DataDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _context.Countries.ToListAsync();
            return Ok(countries);
        }
    }
}