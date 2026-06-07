<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
=======
using System.ComponentModel.DataAnnotations;
>>>>>>> origin/Dev

namespace Shared.DTOs.Basket;

public class BasketItemsDto
{
    public int Id { get; set; }
<<<<<<< HEAD

    public string ProductName { get; set; } = default!;

    public string PictureUrl { get; set; } = default!;
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
=======
    public string ProductName { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;
    public string Brand { get; set; } = default!;
    public string Type { get; set; } = default!;

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

>>>>>>> origin/Dev
    [Range(0, 100)]
    public int Quantity { get; set; }
}
