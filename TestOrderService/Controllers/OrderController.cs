using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestOrderService.Models;
using TestOrderService.Services;

namespace TestOrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderPackingService _orderPackingService;

        public OrderController(OrderPackingService orderPackingService)
        {
            _orderPackingService = orderPackingService;
        }

        [HttpPost("process-orders")]
        public IActionResult ProcessOrders([FromBody] OrdersRequest ordersRequest)
        {
            if (ordersRequest == null || ordersRequest.Orders == null || !ordersRequest.Orders.Any())
            {
                return BadRequest("Invalid order request.");
            }

            var result = _orderPackingService.ProcessOrders(ordersRequest);
            return Ok(result);
        }

    }
}
