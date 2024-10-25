using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Data;

namespace WebApplication1.Controllers;
public class MoviesController : Controller
{
    private readonly AppDbContext _context;
    public MoviesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var allMovies = await _context.Movies
            .Include(m => m.Cinema)
            .OrderByDescending(m => m.StartDate)
            .ToListAsync();
        return View(allMovies);
    }
}
