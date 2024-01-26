using CustomERP.Trucks.Application;
using CustomERP.Trucks.Application.CreateTruck;
using CustomERP.Trucks.Application.DeleteTruck;
using CustomERP.Trucks.Application.GetTruckById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomERP.Api.Trucks
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTruck([FromBody] CreateTruckRequest request)
        {
            var truckId = await sender.Send(new CreateTruckCommand(request.Code, request.Name, request.Description));
            return CreatedAtRoute("GetTruckRoute", new { id = truckId }, null);
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
