using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers
{
    [ApiController]
    [Route("/firstnames")]
    public class FirstnamesController : ControllerBase
    {
        private readonly FirstnamesService _firstnamesSerivce;
        // constructor to add DB context to controller
        public FirstnamesController(FirstnamesService firstnamesService)
        {
            _firstnamesSerivce = firstnamesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFirstnames()
        {
            var countries = await _firstnamesSerivce.GetAllFirstnamesAsync();
            return Ok(countries);
        }
        [HttpGet("random")]
        public async Task<IActionResult> GetRandomFirstname()
        {
            try
            {
                var GetRandomFirstname = await _firstnamesSerivce.GetRandomFirstnameAsync();
                return Ok(GetRandomFirstname);
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }        
        }
    }
}