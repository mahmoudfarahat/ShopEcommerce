

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopEcommerce.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
 
namespace ShopEcommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


            builder.Services.AddScoped<IPieRepository, PieRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IOrderRepository,OrderRepository>();

            builder.Services.AddScoped<IShoppingCart, ShoppingCart>(serviceProvidor => ShoppingCart.GetCart(serviceProvidor));


            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();  

            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                })             
                
                ; // enable mvc 
            builder.Services.AddRazorPages();
            builder.Services.AddRazorComponents().AddInteractiveServerComponents();


            builder.Services.AddDbContext<ShopDbContext>(options =>
            {
                options.UseSqlServer(
                    //builder.Configuration["ConnectionStrings:DefaultConnection"]
                    connectionString
                    );
            });

            builder.Services.AddDefaultIdentity<IdentityUser>(
                //options => options.SignIn.RequireConfirmedAccount = true
                )
                .AddEntityFrameworkStores<ShopDbContext>();

            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");

            app.UseStaticFiles(); // look for request wit static files 
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization(); 
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

            app.UseAntiforgery();

            app.MapRazorPages();

            app.MapRazorComponents<ShopEcommerce.App.App>()
                .AddInteractiveServerRenderMode();


            DbInitializer.Seed(app);

            app.Run();
        }
    }
}
