<<<<<<< HEAD
﻿using Shared.DTOs.Identity;
=======
using Shared.DTOs.Identity;
>>>>>>> origin/Dev

namespace Shared.DTOs.Order;

public class OrderDto
{
    public int DeliveryMethodId { get; set; }
    public string BasketId { get; set; } = default!;
<<<<<<< HEAD
    public AddressDto Address { get; set; } = default!;
=======
    public AddressDto? ShipToAddress { get; set; }
    public AddressDto? Address { get; set; }
>>>>>>> origin/Dev
}
