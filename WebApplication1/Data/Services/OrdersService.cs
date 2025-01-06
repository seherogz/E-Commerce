
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Services;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Data.Services;

public class OrdersService : IOrdersService
{
    readonly AppDbContext _context;

    public OrdersService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
    {
        var orders = await _context.Orders
            .Include(o => o.OderItem).ThenInclude(n => n.Movie)
            .Where(o => o.UserId == userId)
            .ToListAsync();

        return orders;
    }

    public async Task StoreOrderAsync(List<ShoppingCartItem> items, int userId, string email)
    {
        var order = new Order()
        {
            Email = email,
            UserId = userId,
        };

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        foreach (var item in items)
        {
            var orderItem = new OrderItem()
            {
                Amount = item.Amount,
                MovieId = item.Movie.Id,
                OrderId = order.Id,
                Price = item.Movie.Price
            };
            await _context.OrderItem.AddAsync(orderItem);
        }
        await _context.SaveChangesAsync();
    }
}