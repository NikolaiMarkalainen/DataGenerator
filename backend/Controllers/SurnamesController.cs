using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers
{
    [ApiController]
    [Route("/surnames")]
    public class SurnamesController : ControllerBase
    {
        private readonly SurnamesService _surnamesService;
        public SurnamesController(SurnamesService surnamesService)
        {
            _surnamesService = surnamesService;      
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSurnames()
        {
            var countries = await _surnamesService.GetAllSurnamesAsync();
            return Ok(countries);
        }
        [HttpGet("random")]
        public async Task <IActionResult> GetRandomSurname()
        {
            try
            {
                var randomSurname = await _surnamesService.GetRandomSurnameAsync();
                return Ok(randomSurname);
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}