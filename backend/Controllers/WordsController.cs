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
        public async Task<IActionResult> GetAllWords()
        {
            var words = await _wordsService.GetAllWordsAsync();
            return Ok(words);
        }
        [HttpGet("random")]
        public async Task<IActionResult> GetRandomWord()
        {
            try
            {
                var word = await _wordsService.GetRandomWordAsync();
                return Ok(word);
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            } 
        }
    }
}