using Microsoft.AspNetCore.Mvc;
using ShopEcommerce.Models;
using ShopEcommerce.ViewModels;

namespace ShopEcommerce.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly IShoppingCart shoppingCart;

        public ShoppingCartSummary(IShoppingCart shoppingCart)
        {
            this.shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = shoppingCart.GetShoppingCartItems();

            shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel(shoppingCart, shoppingCart.GetShoppingCartTotal());

            return View(shoppingCartViewModel);
        }
    }
}
