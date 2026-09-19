using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models;

public class Category
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters.")]
    public string Name { get; set; } = String.Empty;

    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string? Description { get; set; } = String.Empty;

    public DateTime CreatedAt { get; set; }
};
