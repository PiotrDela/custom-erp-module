using CustomERP.Trucks.Api.Contracts;
using CustomERP.Trucks.Application;
using CustomERP.Trucks.Application.CreateTruck;
using CustomERP.Trucks.Application.DeleteTruck;
using CustomERP.Trucks.Application.GetTruckById;
using CustomERP.Trucks.Application.GetTrucks;
using CustomERP.Trucks.Application.UpdateTruck;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomERP.Trucks.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [TypeFilter<DomainExceptionFilterAttribute>]
    public class TrucksController : Controller
    {
        private readonly ISender sender;

        public TrucksController(ISender sender)
        {
            this.sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        [HttpGet]
        [Route("{id:guid}", Name = "GetTruckRoute")]
        [ProducesResponseType(typeof(TruckDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTruck([FromRoute] Guid id)
        {
            var note = await sender.Send(new GetTruckByIdQuery(id));
            return Ok(note);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTrucks([FromQuery] GetTrucksQueryParameters parameters)
        {
            var trucks = await sender.Send(new GetTrucksQuery(parameters));
            return Ok(trucks);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTruck([FromBody] CreateTruckRequestDto request)
        {
            var truckId = await sender.Send(new CreateTruckCommand(request.Code, request.Name, request.Description));
            return CreatedAtRoute("GetTruckRoute", new { id = truckId }, null);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTruck([FromBody] UpdateTruckRequestDto request)
        {
            await sender.Send(new UpdateTruckCommand(request.Id.Value, request.Code, request.Name, request.Description, request.UsageStatus));
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTruck([FromRoute] Guid id)
        {
            await sender.Send(new DeleteTruckCommand(id));
            return NoContent();
        }
    }
}
