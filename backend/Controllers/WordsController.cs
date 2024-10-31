using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers
{
    [ApiController]
    [Route("/words")]
    public class WordsController : ControllerBase
    {
        private readonly WordsService _wordsService;
        // constructor to add DB context to controller
        public WordsController(WordsService wordsService)
        {
            _wordsService = wordsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _wordsService.GetAllWordsAsync();
            return Ok(countries);
        }
    }
}