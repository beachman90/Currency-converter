using Currency_converter.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Currency_converter.Models;

namespace Currency_converter_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpPost("convert")]
        public async Task<IActionResult> Convert([FromBody] ConversionRequest request)
        {

            request.FromCurrency = request.FromCurrency?.ToUpper();
            request.ToCurrency = request.ToCurrency?.ToUpper();

            try
            {
                decimal result = request.Date.HasValue
                    ? await _currencyService.ConvertCurrencyHistorical(
                        request.Date.Value,
                        request.FromCurrency,
                        request.ToCurrency,
                        request.Amount)
                    : await _currencyService.ConvertCurrency(
                        request.FromCurrency,
                        request.ToCurrency,
                        request.Amount);

                return Ok(new
                {
                    ConvertedAmount = result,
                    FromCurrency = request.FromCurrency,
                    ToCurrency = request.ToCurrency,
                    Date = request.Date,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
