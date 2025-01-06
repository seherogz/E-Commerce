using Microsoft.AspNetCore.Http.Features;
using WebApplication1.Models;

namespace WebApplication1.Data.Services;

public interface IOrdersService
{
    Task StoreOrderAsync(List<ShoppingCartItem> items,int userId, string Email);
    Task<List<Order>> GetOrdersByUserIdAsync(int userId);
}
