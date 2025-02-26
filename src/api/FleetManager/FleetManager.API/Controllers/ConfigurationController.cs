using FleetManager.Domain.SeedWork;
using FleetManager.Domain.SeedWork.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigurationController : ControllerBase
{
    private readonly string? _googleMapsApiKey;

    public ConfigurationController(IConfiguration configuration)
    {
        _googleMapsApiKey = configuration["FleetManager:GoogleMapsApiKey"];
    }

    [HttpGet("googleMapsApiKey")]
    public ActionResult<Result<string>> GetGoogleMapsApiKeyAsync()
    {
        if (string.IsNullOrWhiteSpace(_googleMapsApiKey))
        {
            return NotFound(Result<string>.Failure(new Error("Configuration.GoogleMapsApiKeyIsMissing", "GoogleMapsApiKey is missing.")));
        }
        
        return Ok(Result<string>.Success(_googleMapsApiKey));
    }

}