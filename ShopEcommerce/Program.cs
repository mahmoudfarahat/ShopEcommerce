

using Microsoft.EntityFrameworkCore;
using ShopEcommerce.Models;

namespace ShopEcommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddScoped<IPieRepository, PieRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
           
            builder.Services.AddScoped<IShoppingCart, ShoppingCart>(serviceProvidor => ShoppingCart.GetCart(serviceProvidor));
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();  

            builder.Services.AddControllersWithViews(); // enable mvc 

            builder.Services.AddDbContext<ShopDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration["ConnectionStrings:DefaultConnection"]
                    );
            });
            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");

            app.UseStaticFiles(); // look for request wit static files 
            app.UseSession();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // show exception developer

            }

            //app.MapDefaultControllerRoute(); //routes Views and will be at the end "{controller=Home}/{action=Index}/{id?}"

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //app.MapControllerRoute(
            // name: "default",
            // pattern: "{controller=Home}/{action=Index}/{id:int?}");


            DbInitializer.Seed(app);

            app.Run();
        }
    }
}
