using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Stock.Api.Controllers;

[ApiController]
[Route("api/stocks/{code}")]
[Produces("application/json")]
public class ProductController : ControllerBase
{
    // ── 1) GetAll ──
    [HttpGet]
    [EndpointDescription("Retrieve stock by product code")]
    [EndpointName("GetStockByProductCode")]
    [ProducesResponseType(typeof(IEnumerable<global::Stock.Api.Stock>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult GetAll(
        [Description("Product code. Full match. Required")] string code)
    {
        try
        {
            var stocks = Stocks.Items;
            
            if (!string.IsNullOrEmpty(code))
            {
                stocks = stocks.Where(p => string.Equals(p.ProductCode, code, StringComparison.CurrentCultureIgnoreCase));
            }
            
            return Ok(stocks.FirstOrDefault());
        }
        catch (Exception)
        {
            return Ok("It was not possible to retrieve the product stock");
        }
    }
}