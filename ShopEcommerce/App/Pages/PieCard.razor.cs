using ShopEcommerce.Models;
using Microsoft.AspNetCore.Components;

namespace ShopEcommerce.App.Pages
{
    public partial class PieCard
    {
        [Parameter]
        public Pie? Pie { get; set; }
    }
}
