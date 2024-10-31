using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly CountriesService _countriesService;
        // constructor to add DB context to controller
        public CountriesController(CountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _countriesService.GetAllCountriesAsync();
            return Ok(countries);
        }

        [HttpGet("random")]
        public async Task <IActionResult> GetRandomCountries([FromQuery] int amount = 1)
        {
            try
            {
                var randomCountries = await _countriesService.GetRandomCountriesAsync(amount);
                return Ok(randomCountries);
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}