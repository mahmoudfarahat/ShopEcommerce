namespace ShopEcommerce.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly  ShopDbContext _shopDbContext;
        private readonly IShoppingCart _shoppingCart;

        public OrderRepository( ShopDbContext ShopDbContext, IShoppingCart shoppingCart)
        {
            _shopDbContext = ShopDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            //adding the order with its details

            foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.pie.PieId,
                    Price = shoppingCartItem.pie.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            _shopDbContext.Orders.Add(order);

            _shopDbContext.SaveChanges();
        }
    }
}
