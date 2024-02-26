
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ShopEcommerce.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly ShopDbContext shopDbContext;
        public string? ShoppingCartId { get;  set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default;
        private ShoppingCart(ShopDbContext shopDbContext)
        {
            this.shopDbContext = shopDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            ShopDbContext context = services.GetService<ShopDbContext>() ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartId")?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
       
        public void AddToCart(Pie pie)
        {
            var shoppingCartItem = shopDbContext.ShoppingCartItems.SingleOrDefault(
                    s => s.pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId
                    );

            if( shoppingCartItem == null )
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    pie = pie,
                    Amount = 1
                };
                shopDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            shopDbContext.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = shopDbContext.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);
            shopDbContext.ShoppingCartItems.RemoveRange(cartItems);
            shopDbContext.SaveChanges();

        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??=
                        shopDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                        .Include(s => s.pie)
                        .ToList();
        }

        public decimal GetShoppingCartTotal()
        {
                var total = shopDbContext.ShoppingCartItems.Where( c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.pie.Price * c.Amount).Sum();

            return total;
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem = shopDbContext.ShoppingCartItems.SingleOrDefault(

                s => s.pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if(shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    shopDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            shopDbContext.SaveChanges();
            return localAmount;
        }
    }
}
