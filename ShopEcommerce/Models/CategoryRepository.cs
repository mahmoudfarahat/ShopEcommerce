
namespace ShopEcommerce.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopDbContext shopDbContext;

        public CategoryRepository(ShopDbContext shopDbContext)
        {
            this.shopDbContext = shopDbContext;
        }
        public IEnumerable<Category> AllCategories
        {
            get
            {
                return shopDbContext.Categories.OrderBy(p => p.CategoryName);
            }
        }
    }
}
