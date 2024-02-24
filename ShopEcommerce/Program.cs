 

namespace ShopEcommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews(); // enable mvc 

            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");

            app.UseStaticFiles(); // look for request wit static files 

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // show exception developer

            }
            app.MapDefaultControllerRoute(); //routes Views and will be at the end

            app.Run();
        }
    }
}
