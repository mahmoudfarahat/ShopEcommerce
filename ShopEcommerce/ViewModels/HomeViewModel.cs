﻿using ShopEcommerce.Models;

namespace ShopEcommerce.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel(IEnumerable<Pie> piesOfTheWeek)
        {
            PiesOfTheWeek = piesOfTheWeek;
        }

        public IEnumerable<Pie> PiesOfTheWeek { get;  }

        

    }
}
