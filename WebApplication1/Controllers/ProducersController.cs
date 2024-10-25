using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Data;

namespace WebApplication1.Controllers;
public class ProducersController : Controller
{
    private readonly AppDbContext _context;
    public ProducersController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var allProducers = await _context.Producers.ToListAsync(); //var değişkenimin tipini list olarak tanımladı. Tolist'i silseydim de data tipim Producers olacaktı
        return View(allProducers);
    }
}
