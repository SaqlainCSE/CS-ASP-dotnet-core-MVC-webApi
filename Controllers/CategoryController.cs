using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/categories/")]
    public class CategoryController : ControllerBase
    {
        //In-memory data storage for categories
        private static List<Category> categories = new List<Category>();

        //GET all data
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var allCategories = categories;
            return Ok(allCategories);
        }

        //Get data by ID
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(Guid id)
        {
            var category = categories.FirstOrDefault(categories => categories.Id == id);

            if (category == null)
            {
                return NotFound("Data not found");
            }

            return Ok(category);
        }

        //Get data by search
        [HttpGet("search")]
        public IActionResult GetCategoryBySearch([FromQuery] string search)
        {
            var CategoryBySearch = categories.Where(categories => categories.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            // Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(CategoryBySearch)); //debugging...

            if (CategoryBySearch.Count == 0)
            {
                return BadRequest("Data not found");
            }

            return Ok(CategoryBySearch);
        }

        [HttpPost]
        public IActionResult CreateCategory(Category request)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };

            categories.Add(category);

            return Ok(category);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(Guid id)
        {
            var DeleteCategory = categories.FirstOrDefault(categories => categories.Id == id);

            if (DeleteCategory == null)
            {
                return BadRequest("Data not found");
            }

            categories.Remove(DeleteCategory);

            return Ok("Category Deleted Successfully.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(Guid id, Category request)
        {
            var UpdateCategory = categories.FirstOrDefault(categories => categories.Id == id);

            if (UpdateCategory == null)
            {
                return BadRequest("Data not found");
            }


            // Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(UpdateCategory)); //debugging...

            UpdateCategory.Name = request.Name;
            UpdateCategory.Description = request.Description;
            UpdateCategory.CreatedAt = DateTime.UtcNow;

            return Ok(UpdateCategory);
        }
    
    }
}
