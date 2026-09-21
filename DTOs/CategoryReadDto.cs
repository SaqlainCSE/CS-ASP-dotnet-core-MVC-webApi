using System;

namespace WebAPI.DTOs;

public class CategoryReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string? Description { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; }
}
