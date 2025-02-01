using FleetManager.Application.Vehicles.AddInspection;
using FleetManager.Application.Vehicles.AddRepair;
using FleetManager.Application.Vehicles.CreateVehicle;
using FleetManager.Application.Vehicles.GetVehicleManagement;
using FleetManager.Application.Vehicles.GetVehicles;
using FleetManager.Application.Vehicles.Shared;
using FleetManager.Domain.SeedWork.Results;
using FleetManager.Domain.Vehicles.Models;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace FleetManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetAsync([FromQuery] VehiclesFilterDto vehiclesFilterDto, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetVehiclesQuery(vehiclesFilterDto), cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<Result>> CreateVehicleAsync([FromBody] CreateVehicleRequest request, CancellationToken cancellationToken)
        {
            Result result = await _mediator.Send(new CreateVehicleCommand(request), cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("management/{id}")]
        public async Task<ActionResult<VehicleManagementDto>> GetVehicleManagementAsync(string id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetVehicleManagementQuery(Guid.Parse(id)), cancellationToken));
        }

        [HttpPost("management/repair")]
        public async Task<ActionResult> AddRepairAsync([FromBody]RepairDto repairRequest, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new AddRepairCommand(repairRequest), cancellationToken));
        }

        [HttpPost("management/inspection")]
        public async Task<ActionResult> AddInspectionAsync([FromBody] InspectionDto addInspectionRequest, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new AddInspectionCommand(addInspectionRequest), cancellationToken));
        }
    }
}
