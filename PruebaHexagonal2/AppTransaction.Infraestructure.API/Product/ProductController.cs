using AppTransaction.Aplication.Interfaces;
using AppTransaction.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AppTransaction.Infraestructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;         
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(2000, cancellationToken);
            var allClients = await _productService.GetAsync();
            return Ok(allClients);
        }

        [HttpGet("{productId}")]
        public async Task <IActionResult> GetByID(Guid productId, CancellationToken cancellationToken)
        {
            await Task.Delay(2000, cancellationToken);
            var ExpectedProduct = await _productService.GetByIdAsync(productId);
            return Ok(ExpectedProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product, CancellationToken cancellationToken)
        {
            await Task.Delay(2000, cancellationToken);
            await _productService.SaveAsync(product);
            return Ok("Added product");
        }
    }
}
