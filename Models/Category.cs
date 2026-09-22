using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string? Description { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; }
};
