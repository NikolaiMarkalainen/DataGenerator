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
        public async Task<IActionResult> ProcessDataToFile([FromBody] FileRequest request)
        {
            if(request == null)
            {
                return BadRequest("Cant generate file without data.");
            }
            try
            {
                var result = await _generateRandomService.GenerateObjectToData(request);
                return Ok(new {result});
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
/*

{
    amount: 1000,
    variables:
    [
        {"name":"asdas","type":0,"variableData":{"min":0,"max":10,"decimalPrecision":0,"decimal":false}},
        {"name":"asdasd","type":2,"variableData":{"fixedString":"dsdsds"}},
        {"name":"O","type":1,"variableData":{"characterLength":0,"words":false}},
        {"name":"Random First Name","type":3,"variableData":{"useProperty":true}},
        {"name":"Random Last name","type":4,"variableData":{"useProperty":true}},
        {"name":"Random Country","type":5,"variableData":{"text":"Bulgaria","key":"BG","fixed":false,"amountFixed":1}},
        {"name":"CUstom Object","type":6,"variableData":{"fields":[{"name":"asdasd","type":5,"variableData":{"fixed":false,"amountFixed":1}}]}},
        {"name":"Random Id","type":7,"variableData":{"idType":2}}
    ]
}

*/