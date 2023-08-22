using Microsoft.AspNetCore.Mvc;
using ShoppingListServer.Models;
using ShoppingListServer.Services;

namespace ShoppingListServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly ItemsService _itemsService;

        public ItemsController(ItemsService itemsService) =>
            _itemsService = itemsService;

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
        {
            var items = await _itemsService.GetAllItemsAsync();
            return Ok(items);
        }

        // GET: api/items/{id}
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Item>> GetItemById(string id)
        {
            var item = await _itemsService.GetItemByIdAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST: api/items
        [HttpPost]
        public async Task<ActionResult<Item>> CreateItem(CreateItem createItem)
        {
            var newItem = new Item { 
                OrderId = createItem.OrderId,
                CategoryId = createItem.CategoryId,
                ItemName= createItem.ItemName,
                Quantity= 1
            };
            var createdItem = await _itemsService.CreateItemAsync(newItem);
            return CreatedAtAction(nameof(GetItemById), new { id = createdItem.ItemId }, createdItem);
        }

        // PUT: api/items/{id}
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateItem(string id, Item updatedItem)
        {
            var item = await _itemsService.UpdateItemAsync(id, updatedItem);

            if (item is null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            var deleted = await _itemsService.DeleteItemAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}