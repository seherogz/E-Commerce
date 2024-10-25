using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Data;
using WebApplication1.Data.Services;

namespace WebApplication1;
public class Program
{


    public static void Main(String[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddScoped<IActorsServices, ActorsService>(); //() bu new'lememizi sağlıyor.Artık IActorService istendiğinde ActorService'yi gönder.

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

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        AppDbInitiliazer.Seed(app);
        app.Run();
    }
}



