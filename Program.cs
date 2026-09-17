var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

//In-memory data storage for categories
List<Category> categories = new List<Category>();

//API Endpoints for Categories

//GET all data
app.MapGet("/api/categories", () =>
{
    return Results.Ok(categories);
});

//GET data by Search
app.MapGet("/api/categories/search", (string? search) =>
{

    if (string.IsNullOrEmpty(search))
    {
        Console.WriteLine(search);
        return Results.Ok("Data Not Found");
    }

    var filteredData = categories.Where(data =>
        (data.Name != null && data.Name.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
        (data.Description != null && data.Description.Contains(search, StringComparison.OrdinalIgnoreCase))
    ).ToList();

    return Results.Ok(filteredData);
});

//GET data by ID
app.MapGet("/api/categories/{id}", (Guid id) =>
{
    var category = categories.FirstOrDefault(categories => categories.CategoryId == id);

    if (category == null)
    {
        return Results.NotFound("Category Id Not Found");
    }

    return Results.Ok(category);
});

//Create data
app.MapPost("/api/categories", (Category category) =>
{
    var newCategory = new Category
    {
        CategoryId = Guid.NewGuid(),
        Name = category.Name,
        Description = category.Description,
        CreatedAt = DateTime.UtcNow
    };

    categories.Add(newCategory);

    return Results.Created($"/api/categories/{newCategory.CategoryId}", newCategory);
});

//Update data
app.MapPut("/api/categories/{id}", (Guid id, Category category) =>
{
    var categoryToUpdate = categories.FirstOrDefault(categories => categories.CategoryId == id);

    if (categoryToUpdate == null)
    {
        return Results.NotFound("Category Id Not Found");
    }

    categoryToUpdate.Name = category.Name;
    categoryToUpdate.Description = category.Description;
    categoryToUpdate.CreatedAt = DateTime.UtcNow;

    return Results.Ok(categoryToUpdate);
});

//Delete data
app.MapDelete("/api/categories/{id}", (Guid id) =>
{
    Console.WriteLine(id);

    var categoryToDelete = categories.FirstOrDefault(categories => categories.CategoryId == id);

    if (categoryToDelete == null)
    {
        return Results.NotFound("Category Id Not Found");
    }

    categories.Remove(categoryToDelete);

    return Results.Ok(categories);

});

app.Run();

//Category Model
public record Category
{
    public Guid CategoryId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
};

