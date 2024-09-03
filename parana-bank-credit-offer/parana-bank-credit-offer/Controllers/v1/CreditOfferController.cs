using Microsoft.AspNetCore.Mvc;

namespace parana_bank_credit_offer.Controllers.v1
{
    [ApiController]
    [Route("[controller]")]
    public class CreditOfferController : ControllerBase
    {

        private readonly ILogger<CreditOfferController> _logger;

        public CreditOfferController(ILogger<CreditOfferController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SendNewCreditOfferAsync()
        {
            return Ok();
        }
    }
}
