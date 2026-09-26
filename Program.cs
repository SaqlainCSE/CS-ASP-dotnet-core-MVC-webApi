using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Data Annotations...............

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
                            .Where(e => e.Value != null && e.Value.Errors.Count > 0)
                            .Select(e => new
                            {
                                Field = e.Key,
                                Errors = e.Value != null ? e.Value.Errors.Select(x => x.ErrorMessage).ToArray() : new string[0]
                            }).ToList();

        var errorString = string.Join("; ", errors.Select(e => $"{e.Field}: {string.Join(", ", e.Errors)}"));
        return new BadRequestObjectResult(new
        {
            Message = "Validation Failed",
            Errors = errorString
        });
    };
});

//Data Annotations End...........

var app = builder.Build();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();

