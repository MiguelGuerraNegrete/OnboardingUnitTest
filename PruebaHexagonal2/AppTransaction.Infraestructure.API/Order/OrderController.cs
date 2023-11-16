using AppTransaction.Aplication.Interfaces;
using AppTransaction.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace AppTransaction.Infraestructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
            
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;  
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(2000, cancellationToken);
            var allOrdes = await _orderService.GetAsync();
            return Ok(allOrdes);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetByID(Guid orderId, CancellationToken cancellationToken)
        {
            await Task.Delay(2000, cancellationToken);
            var ExpectedOrder = await _orderService.GetByAsync(orderId);
            return Ok(ExpectedOrder);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order, CancellationToken cancellationToken)
        {
            await Task.Delay(2000, cancellationToken);
            await _orderService.SaveAsync(order);
            return Ok("Added order");
        }
    }
}
