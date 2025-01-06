using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data.Cart;

public class ShoppingCart
{
    public AppDbContext _context { get; set; }


    public ShoppingCart (AppDbContext context)
    {
        _context = context;
    }

    public void AddItemToCart(Movie movie)
    {
        var shoppingCartItem = _context.ShoppingCardItems.FirstOrDefault(x => x.Movie.Id == movie.Id
        && x.ShoppingCardId == ShoppingCartId); //birinci koşulda benim verdiğim id ile o filmin idsi tutuyor mu onu kontrol ediyorum.  ikinci koşulda verilen sepet ıd'deki sepette olup olmadığımı kontrol ediyorum.

        if (shoppingCartItem is null )
        {
            shoppingCartItem = new ShoppingCartItem()
            {
                ShoppingCardId = ShoppingCartId,
                Movie = movie,
                Amount = 1,

            };

            _context.ShoppingCardItems.Add(shoppingCartItem);
        }

        else
        {
            shoppingCartItem.Amount++;
        }

        _context.SaveChanges();
    }

    public static ShoppingCart GetShoppingCart(IServiceProvider services) //bana servis sağlar.
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;  //ihtiyaç duyduğum servisi getiriyor. HttpContextAccessor içindeki httpContexte ihtyiaç duyuyorum.
        var context = services.GetService<AppDbContext>();

        string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString(); //sessionda bilgiler string formunda tutulur.
        //guid rastgele benzersiz kimlikler verir. databasedeki newId() gibi
        session.SetString("CartId", cartId);

        return new ShoppingCart(context) { ShoppingCartId = cartId };
    }

    public void RemoveItemFromCart(Movie movie)
    {
        var shoppingCartItem = GetShoppinCartItems()
            .FirstOrDefault(x => x.Movie.Id == movie.Id
         && x.ShoppingCardId == ShoppingCartId); 

        if (shoppingCartItem is not null)
        {
            if (shoppingCartItem.Amount > 1)
            {
                shoppingCartItem.Amount--;
            }
            else
            {
               _context.ShoppingCardItems.Remove(shoppingCartItem);
            }
        }
        _context.SaveChanges();
    }


    public String ShoppingCartId { get; set; }

    public List<ShoppingCartItem> ShoppingCardItems { get; set; }

    public List<ShoppingCartItem> GetShoppinCartItems()
    {
        return ShoppingCardItems ?? (ShoppingCardItems = _context.ShoppingCardItems
            .Where(n=>n.ShoppingCardId == ShoppingCartId)
            .Include(n=>n.Movie).ToList());
    }

    public double GetShoppingCartTotal()
    {
        return _context.ShoppingCardItems
            .Where(n => n.ShoppingCardId == ShoppingCartId)
            .Select(m => m.Movie.Price * m.Amount).Sum();
    }

    public async Task ClearShoppingCartAsync()
    {
        var items = await _context.ShoppingCardItems
            .Where(n => n.ShoppingCardId== ShoppingCartId)
            .ToListAsync();

        _context.ShoppingCardItems.RemoveRange(items);
        await _context.SaveChangesAsync();
    }


 

}

