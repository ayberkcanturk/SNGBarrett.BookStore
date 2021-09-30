using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SNGBarrett.BookStore.Application.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace SNGBarrett.BookStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ApiControllerBase
    {
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Description = "Create a new order", OperationId = "order", Tags = new []{ "create", "order" })]

        public async Task<ActionResult<int>> Order([FromBody]CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = await Mediator.Send(command, cancellationToken);

            return Created("/", orderId);
        }

        [HttpPut]
        [Route("{OrderId}/cancel")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Description = "Cancel an existing order", OperationId = "cancel-order", Tags = new[] { "cancel", "order" })]
        public async Task<ActionResult<int>> Cancel([FromRoute]CancelOrderCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return Accepted("/", result);
        }
    }
}
