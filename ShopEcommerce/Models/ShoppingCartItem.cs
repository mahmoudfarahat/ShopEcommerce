﻿namespace ShopEcommerce.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public Pie pie { get; set; } = default;
        public int Amount { get; set; } 
        public string? ShoppingCartId { get; set; }
    }
}
