﻿using ShopEcommerce.Models;

namespace ShopEcommerce.ViewModels
{
    public class PieListViewModel
    {
        public PieListViewModel(IEnumerable<Pie> pies,string? currentCategory)
        {
            Pies = pies;
            CurrentCategory = currentCategory;
        }

        public IEnumerable<Pie> Pies { get; }
        public string? CurrentCategory { get; }
    }
}
