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
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _firstnamesSerivce.GetAllFirstnamesAsync();
            return Ok(countries);
        }
    }
}