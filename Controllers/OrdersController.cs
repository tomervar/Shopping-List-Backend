using Microsoft.AspNetCore.Mvc;
using ShoppingListServer.Models;
using ShoppingListServer.Services;

namespace ShoppingListServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersService _ordersService;

        public OrdersController(OrdersService ordersService) =>
            _ordersService = ordersService;

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            var orders = await _ordersService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // GET: api/orders/{id}
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Order>> GetOrderById(string id)
        {
            var order = await _ordersService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder()
        {
            var order = new Order { Items = new List<Item>() };
            var createdOrder = await _ordersService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
        }

        // PUT: api/orders/{id}
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateOrder(string id, Order order)
        {
            var updatedOrder = await _ordersService.UpdateOrderAsync(id, order);

            if (updatedOrder == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            var deleted = await _ordersService.DeleteOrderAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
