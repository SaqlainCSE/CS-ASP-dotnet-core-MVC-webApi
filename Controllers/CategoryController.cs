using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
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
            var allCategories = categories.Select(categories => new CategoryReadDto
            {
                Id = categories.Id,
                Name = categories.Name,
                Description = categories.Description,
                CreatedAt = DateTime.UtcNow

            }).ToList();

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

            var categoryData = new CategoryReadDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                CreatedAt = category.CreatedAt
            };

            return Ok(categoryData);
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
        public IActionResult CreateCategory(CategoryCreateDto request)
        {
            var newCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };

            categories.Add(newCategory);

            var CategoryRead = new CategoryReadDto
            {
                Id = newCategory.Id,
                Name = newCategory.Name,
                Description = newCategory.Description,
                CreatedAt = DateTime.UtcNow
            };

            return Ok(CategoryRead);
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
        public IActionResult UpdateCategory(Guid id, CategoryUpdateDto request)
        {
            var UpdateCategory = categories.FirstOrDefault(categories => categories.Id == id);

            if (UpdateCategory == null)
            {
                return BadRequest("Data not found");
            }

            UpdateCategory.Name = request.Name;
            UpdateCategory.Description = request.Description;


            // Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(UpdateCategory)); //debugging...

            var CategoryReadDto = new CategoryReadDto
            {
                Name = UpdateCategory.Name,
                Description = UpdateCategory.Description
            };

            

            return Ok(CategoryReadDto);
        }
    
    }
}
