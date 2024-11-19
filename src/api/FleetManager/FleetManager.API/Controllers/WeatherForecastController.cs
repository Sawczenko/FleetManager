using FleetManager.Domain.Aggregates.Vehicles;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.API.Controllers
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
        private readonly IVehicleRepository _vehicleRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IVehicleRepository vehicleRepository)
        {
            _logger = logger;
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet(Name = "GetVehicles")]
        public async Task<List<Vehicle>> Get(CancellationToken cancellationToken)
        {
            return await _vehicleRepository.GetVehiclesAsync(cancellationToken);
        }
    }
}
