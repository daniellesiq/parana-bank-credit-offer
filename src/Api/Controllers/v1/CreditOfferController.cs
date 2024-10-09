using Domain.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace parana_bank_credit_offer.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/")]
    public class CreditOfferController : ControllerBase
    {
        private readonly IPublishEndpoint _publisher;
        private readonly ILogger<CreditOfferController> _logger;

        public CreditOfferController(IPublishEndpoint publisher, ILogger<CreditOfferController> logger)
        {
            _publisher = publisher;
            _logger = logger;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "new credit offer", Description = "Insert new credit offer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendNewCreditOfferAsync([FromBody] ClientOfferEvent input, CancellationToken cancellationToken)
        {
            if (input != null)
            {
                await _publisher.Publish<ClientOfferEvent>(input, cancellationToken);

                _logger.LogInformation($"Sent event: {nameof(ClientOfferEvent.CorrelationId)}");

                return Ok();
            }
            return BadRequest();
        }
    }
}
