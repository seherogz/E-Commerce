using WebApplication1.Data;
using WebApplication1.Data.Services;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Cart;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ECommerce;

public class Program
{
    public static void Main(String[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        //var connectionString= builder.Configuration.GetConnectionString(ConnectionStrings:DefaultConnectionString);

        // Add services to the container.
        
        builder.Services.AddScoped<IActorsService, ActorsService>(); //birisi senden IActorsService çağırdıysa ActorsService vereceksin.
        builder.Services.AddScoped<IProducersService, ProducersService>();
        builder.Services.AddScoped<ICinemasService, CinemasService>();
        builder.Services.AddScoped<IMoviesService, MoviesService>();
        builder.Services.AddScoped<IOrdersService, OrdersService>();


        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
       .AddEntityFrameworkStores<AppDbContext>()
       .AddDefaultTokenProviders();


        //authentication:gelen kişinin kim olduğu bilgisini almak onu tanımak. Kimlik doğrulaması
        //authorization: kişinin neye yetkili olduğunu anlamak ve ona göre işlem yapmak. User mı admin mi olduğunu anlar.
        builder.Services.AddMemoryCache();
        builder.Services.AddSession();
        builder.Services.AddAuthentication(option => 
            {
                option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

             });


        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer("Data Source=SEHEROUZFDCC\\SQLEXPRESS;Initial Catalog=ECommerceDb;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True "));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseSession();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        AppDbInitiliazer.Seed(app);
        AppDbInitiliazer.SeedUsersAndRoleSAsync(app).GetAwaiter().GetResult();

        app.Run();
    }
}