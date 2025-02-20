using InsideTeste.Database.Enumerator;
using InsideTeste.Models;
using Microsoft.AspNetCore.Mvc;

namespace InsideTeste.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController(ILogger<OrdersController> logger, IOrderService orderService) : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger = logger;
        private readonly IOrderService _orderService = orderService;

        [HttpPost()]
        public async Task<IActionResult> NewOrder()
        {
            return Ok(await _orderService.RegistryNewOrder());
        }

        [HttpPost("product-to-order")]
        public async Task<IActionResult> AddProducts([FromBody] ProductOrder productOrder)
        {
            return Ok(await _orderService.AddProductToOrder(productOrder));
        }

        [HttpDelete("product-from-order")]
        public async Task<IActionResult> RemoveProducts([FromBody] ProductOrder productOrder)
        {
            return Ok(await _orderService.RemoveProductFromOrder(productOrder));
        }

        [HttpPut("close-order")]
        public async Task<IActionResult> CloseOrder([FromQuery] Guid orderId)
        {
            return Ok(await _orderService.CloseOrder(orderId));
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders([FromQuery] EOrderStatus? orderStatus)
        {
            return Ok(await _orderService.GetOrders(orderStatus));
        }

        [HttpGet("order-and-product/{orderId}")]
        public async Task<IActionResult> GetOrderAndProducts([FromRoute] Guid orderId)
        {
            return Ok(await _orderService.GetOrderAndProducts(orderId));
        }
    }
}
