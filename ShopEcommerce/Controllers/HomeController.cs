using Microsoft.AspNetCore.Mvc;
using ShopEcommerce.Models;
using ShopEcommerce.ViewModels;

namespace ShopEcommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository pieRepository;

        public HomeController(IPieRepository pieRepository)
        {
            this.pieRepository = pieRepository;
        }

        public IActionResult Index()
        {
            var piesOftheWeek = pieRepository.PiesOfTheWeek;
            var homeViewModel = new HomeViewModel(piesOftheWeek);
         
            return View(homeViewModel);
        }
    }
}
