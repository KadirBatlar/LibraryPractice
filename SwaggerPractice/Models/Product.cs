using System;
using System.Collections.Generic;

namespace SwaggerPractice.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime? Date { get; set; }

    public string? Category { get; set; }
}