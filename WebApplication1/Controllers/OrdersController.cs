using Azure;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Cart;
using WebApplication1.Data.Services;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers;

public class OrdersController : Controller
{
    private readonly IMoviesService _movieService;
    private readonly IOrdersService _orderService;

    private readonly ShoppingCart _shoppingCart;

    public OrdersController(IMoviesService movieService, ShoppingCart shoppingCart, IOrdersService orderService)
    {
        _movieService = movieService;
        _shoppingCart = shoppingCart;
        _orderService = orderService;
    }

    public IActionResult ShoppingCart()
    {
        var items = _shoppingCart.GetShoppinCartItems();
        _shoppingCart.ShoppingCardItems = items; //ShoppingCardItems'te dönecek olan listeyi items'e atamış oldum.

        var response = new ShoppingCartVM()
        {
            ShoppingCart = _shoppingCart,
            ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
        };


        return View(response);
    }

    public async Task<RedirectToActionResult> AddItemToShoppingCart(int id) //RedirectTo.. ActionResult'tan miras alır.
    {
        var item = await _movieService.GetMovieByIdAsync(id);
        if (item is not null)
        {
            _shoppingCart.AddItemToCart(item);
        }

        return RedirectToAction(nameof(ShoppingCart));

    }

    public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
    {
        var item = await _movieService.GetMovieByIdAsync(id);
        if (item is not null)
        {
            _shoppingCart.RemoveItemFromCart(item);
        }

        return RedirectToAction(nameof(ShoppingCart));

    }

    public async Task<IActionResult> CompleteOrder()
    {
        var item = _shoppingCart.GetShoppinCartItems();
        int userId = 1;
        string email = "";

        await _orderService.StoreOrderAsync(item, userId, email);
        await _shoppingCart.ClearShoppingCartAsync();
        return View(item);

    }

    public async Task<IActionResult> Index()
    {
        int userId = 1;
        var order = await _orderService.GetOrdersByUserIdAsync(userId);
        return View(order);
    }

}
