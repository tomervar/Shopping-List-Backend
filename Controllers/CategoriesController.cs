using Microsoft.AspNetCore.Mvc;
using ShoppingListServer.Models;
using ShoppingListServer.Services;

namespace ShoppingListServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesService _categoriesService;

        public CategoriesController(CategoriesService categoriesService) =>
            _categoriesService = categoriesService;


        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var categories = await _categoriesService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/categories/{id}
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Category>> GetCategoryById(string id)
        {
            var category = await _categoriesService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // POST: api/categories
        [HttpPost]
        //public async Task<ActionResult<Category>> CreateCategory(Category category)
        public async Task<ActionResult<Category>> CreateCategory(CreateCategory createCategory)
        {
            var category = new Category { CategoryName = createCategory.CategoryName, CategoryImgLocation = createCategory.CategoryImgLocation };
            var createdCategory = await _categoriesService.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.CategoryId }, createdCategory);
        }

        // PUT: api/categories/{id}
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateCategory(string id, Category category)
        {
            var updatedCategory = await _categoriesService.UpdateCategoryAsync(id, category);

            if (updatedCategory == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/categories/{id}
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var deleted = await _categoriesService.DeleteCategoryAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
