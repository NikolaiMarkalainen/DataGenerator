using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("/generate")]
    public class GenerateDataController: ControllerBase
    {
        private readonly GenerateRandomService _generateRandomService;
        public GenerateDataController(GenerateRandomService generateRandomService)
        {
            _generateRandomService = generateRandomService;
        }

        [HttpPost("number")]
        public async Task<IActionResult> ProcessNumberVariable([FromBody] NumberVariable numberVariable)
        {
            if (numberVariable == null)
            {
                return BadRequest("Number variable must be provided.");
            }
            try
            {
                var number = await _generateRandomService.GenerateRandomNumber(numberVariable);
                return Ok(new { number });
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Route("word")]
        [HttpPost]
        public async Task<IActionResult> ProcessRandomString([FromBody] string charactersLength)
        {
            if(charactersLength == null)
            {
                Console.WriteLine("HERE");
                return BadRequest("charactersLength variable must be provided.");
            }
            try
            {
                var word = await _generateRandomService.GenerateRandomString(charactersLength);
                return Ok(new {word});
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Route("id")]
        [HttpPost]
        public async Task<IActionResult> ProcessRandomId([FromBody] IdObject idObject)
        {
            if(idObject == null)
            {
                return BadRequest("IdObject variable must be provided.");
            }
            try
            {
                var id = await _generateRandomService.GenerateRandomId(idObject.IdType);
                return Ok(new {id});
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Route("file")]
        [HttpPost]
        public async Task<IActionResult> ProcessDataToFile([FromBody] Variable variable)
        {
            if(variable == null)
            {
                return BadRequest("Cant generate file without data.");
            }
            try
            {
                var result = await _generateRandomService.GenerateObjectToData(variable);
                return Ok(new {result});
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
