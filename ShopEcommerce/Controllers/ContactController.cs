﻿using Microsoft.AspNetCore.Mvc;

namespace ShopEcommerce.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
