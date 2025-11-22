using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SwaggerPractice.Models;

public partial class Product
{
    /// <summary>
    /// product id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// product name
    /// </summary>
    [Required]
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime? Date { get; set; }

    public string? Category { get; set; }
}
