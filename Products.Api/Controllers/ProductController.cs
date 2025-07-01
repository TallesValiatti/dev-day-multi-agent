using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace OverviewAzureAiAgentService.SalesApi.Controllers;

[ApiController]
[Route("api/products")]
[Produces("application/json")]
public class ProductController : ControllerBase
{
    // ── 1) GetAll ──
    [HttpGet]
    [EndpointDescription("Retrieve all products by params")]
    [EndpointName("GetAllProductsByParam")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult GetAll(
        [Description("Product name. Partial match. Optional")] string? name,
        [Description("Product name. Partial match. Optional")] string? code)
    {
        try
        {
            var books = Products.Items;
            
            if (!string.IsNullOrEmpty(name))
            {
                books = books.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }
            
            if (!string.IsNullOrEmpty(code))
            {
                books = books.Where(p => p.Code.Contains(code, StringComparison.OrdinalIgnoreCase));
            }
            
            return Ok(books);
        }
        catch (Exception)
        {
            return Ok("It was not possible to retrieve the list of books");
        }
    }
}