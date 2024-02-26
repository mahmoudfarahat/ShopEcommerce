using Microsoft.AspNetCore.Mvc;
using ShopEcommerce.Models;
using ShopEcommerce.ViewModels;

namespace ShopEcommerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepository pieRepository;
        private readonly IShoppingCart shoppingCart;

        public ShoppingCartController(IPieRepository pieRepository, IShoppingCart shoppingCart)
        {
            this.pieRepository = pieRepository;
            this.shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
 var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel(shoppingCart,shoppingCart.GetShoppingCartTotal());    
            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int pieId)
        {
            var selectedPie = pieRepository.AllPies.FirstOrDefault(p => p.PieId == pieId);
            if (selectedPie != null)
            {
                shoppingCart.AddToCart(selectedPie);
            }

            return RedirectToAction("Index");
        }


        public RedirectToActionResult RemoveFromShoppingCart(int pieId) { 
        var selectedPie = pieRepository.AllPies.FirstOrDefault(p => p.PieId==pieId);
            if (selectedPie != null)
            {
                shoppingCart.RemoveFromCart(selectedPie);
            }

            return RedirectToAction("Index");
        }

    }
}
