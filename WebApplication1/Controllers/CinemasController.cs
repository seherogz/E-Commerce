using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Data;

namespace WebApplication1.Controllers;

public class CinemasController : Controller
{
    private readonly AppDbContext _context;
    public CinemasController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var  allCinemas = await _context.Cinemas.ToListAsync();
        return View(allCinemas);
    }
}
