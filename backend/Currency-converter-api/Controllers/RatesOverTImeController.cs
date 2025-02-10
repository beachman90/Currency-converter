using Currency_converter.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Currency_converter_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesOverTImeController : ControllerBase
    {                
            private readonly ICurrencyService _currencyService;

            public RatesOverTImeController (ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("rates")]
        public async Task<IActionResult> GetExhangeRatesOverTime(
            [FromQuery] string currency,
            [FromQuery] DateTime timeStart,
            [FromQuery] DateTime timeEnd)
        {
            var rates = await _currencyService.GetExchangeRatesOverTime(currency, timeStart, timeEnd);
            return Ok(rates);
        }
            
            
    }
}
