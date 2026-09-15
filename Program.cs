var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

List<Category> categories = new List<Category>();

//API Endpoints for Categories
app.MapGet("/api/categories", () =>
{
    return Results.Ok(categories);
});

app.MapGet("/api/category", () =>
{
    var category = categories.FirstOrDefault(categories => categories.CategoryId == Guid.Parse("73027d9f-be04-48a4-a17d-34bbb56cda09"));

    if (category == null)
    {
        return Results.NotFound("Category Id Not Found");
    }

    return Results.Ok(category);
});

app.MapPost("/api/categories", () =>
{
    var newCategory = new Category
    {
        CategoryId = Guid.NewGuid(),
        Name = "Electronics",
        Description = "New Devices",
        CreatedAt = DateTime.UtcNow
    };

    categories.Add(newCategory);

    return Results.Created($"/api/categories/{newCategory.CategoryId}", newCategory);
});

app.MapPut("/api/categories", () =>
{
    var categoryToUpdate = categories.FirstOrDefault(categories => categories.CategoryId == Guid.Parse("f6f6f4fb-1b1e-42da-84b2-660748c00c2c"));

    if (categoryToUpdate == null)
    {
        return Results.NotFound("Category Id Not Found");
    }

    categoryToUpdate.Name = "Updated Electronics";
    categoryToUpdate.Description = "Updated New Devives";
    categoryToUpdate.CreatedAt = DateTime.UtcNow;

    return Results.Ok(categoryToUpdate);
});

app.MapDelete("/api/categories", () =>
{
    var categoryToDelete = categories.FirstOrDefault(categories => categories.CategoryId == Guid.Parse("90f5c03f-67e8-468a-bccb-cfa70c4ea0a2"));

    if (categoryToDelete == null)
    {
        return Results.NotFound("Category Id Not Found");
    }

    categories.Remove(categoryToDelete);

    return Results.Ok(categories);

});

app.Run();

public record Category
{
    public Guid CategoryId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
};

