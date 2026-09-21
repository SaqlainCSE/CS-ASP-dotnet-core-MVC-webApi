using System;

namespace WebAPI.DTOs;

public class CategoryCreateDto
{
    public string Name { get; set; } = String.Empty;
    public string? Description { get; set; } = String.Empty;
}
