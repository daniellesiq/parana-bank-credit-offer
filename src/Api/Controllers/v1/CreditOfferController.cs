using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace parana_bank_credit_offer.Controllers.v1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/")]
    public class CreditOfferController : ControllerBase
    {

        private readonly ILogger<CreditOfferController> _logger;

        public CreditOfferController(ILogger<CreditOfferController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "new credit offer", Description = "Insert new credit offer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendNewCreditOfferAsync()
        {
            return Ok();
        }
    }
}
