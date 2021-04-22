using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Orders.v1.Commands.Checkout;
using Order.Application.Orders.v1.Commands.Update;
using Order.Application.Orders.v1.Queries;
using Order.Application.Orders.v1.Queries.Delete;
using Order.Application.Orders.v1.Queries.Get;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrders(string username)
        {
            List<OrderResponseDto> orders = await _mediator.Send(new GetQuery { Username = username });

            if (orders == null)
                return NotFound();

            return Ok(orders);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderResponseDto>> Checkout([FromBody] CheckoutCommand command)
        {
            var result = await _mediator.Send(command);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update([FromBody] UpdateCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            var query = new DeleteQuery { Id = id };
            await _mediator.Send(query);
            return NoContent();
        }
    }
}
