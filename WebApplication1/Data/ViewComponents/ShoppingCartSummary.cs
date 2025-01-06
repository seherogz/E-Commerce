
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Cart;

namespace WebApplication1.Data.ViewComponents;

public class ShoppingCartSummary : ViewComponent
{
    private readonly ShoppingCart _shoppingCart;

    public ShoppingCartSummary(ShoppingCart shoppingCart)
    {
        _shoppingCart = shoppingCart; 
    }

    public IViewComponentResult Invoke()
    {
        var items = _shoppingCart.GetShoppinCartItems();
        return View(items.Count);

    }
}
