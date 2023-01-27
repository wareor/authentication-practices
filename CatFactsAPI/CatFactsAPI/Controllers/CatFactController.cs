using CatFactsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatFactsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatFactController : ControllerBase
    {
        private CatFactExternalAPI _catFactService;

        public CatFactController()
        {
            this._catFactService = new CatFactExternalAPI("https://catfact.ninja/");
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await this._catFactService.Get());
        }
    }
}


/*
 
 using Microsoft.AspNetCore.Mvc;

namespace CatFactsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}*/