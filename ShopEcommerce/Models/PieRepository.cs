
using Microsoft.EntityFrameworkCore;

namespace ShopEcommerce.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly ShopDbContext shopDbContext;

        public PieRepository(ShopDbContext shopDbContext)
        {
            this.shopDbContext = shopDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return shopDbContext.Pies.Include(a => a.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return shopDbContext.Pies.Include(a => a.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie? GetPieById(int pieId)
        {
            return shopDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);    
        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
             return shopDbContext.Pies.Where(p => p.Name.Contains(searchQuery));   
        }
    }
}
