using WebApplication1.Data.Services;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class MoviesController : Controller
{
    readonly IMoviesService _service;

    public MoviesController(IMoviesService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var allMovies = await _service.GetAllAsync(m => m.Cinema);
        return View(allMovies);
    }


    public async Task<IActionResult> Details(int id)
    {
        var movieDetail = await _service.GetMovieByIdAsync(id);
        return View(movieDetail);
    }

    public async Task<IActionResult> Create()
    {
        var movieDropdowns = await _service.GetMovieDropdownsValuesAsync();

        ViewBag.Cinemas = new SelectList(movieDropdowns.Cinemas, "Id", "Name");
        ViewBag.Producers = new SelectList(movieDropdowns.Producers, "Id", "FullName");
        ViewBag.Actors = new SelectList(movieDropdowns.Actors, "Id", "FullName");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(MovieVM movie)
    {
        if (!ModelState.IsValid)
        {
            return View(movie);
        }

        await _service.AddMovieAsync(movie);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var existingMovie = await _service.GetMovieByIdAsync(id);
        if (existingMovie == null) { return View("_NotFound"); }

        var response = new MovieVM()
        {
            Id = existingMovie.Id,
            CinemaId = existingMovie.CinemaId,
            Description = existingMovie.Description,
            ImageUrl = existingMovie.ImageUrl,
            EndDate = existingMovie.EndDate,
            MovieCategory = existingMovie.MovieCategory,
            Name = existingMovie.Name,
            Price = existingMovie.Price,
            ProducerId = existingMovie.ProducerId,
            StartDate = existingMovie.StartDate,
            ActorIds = existingMovie.Actors_Movies.Select(a => a.ActorId).ToList(),
        };

        var movieDropdowns = await _service.GetMovieDropdownsValuesAsync();


        ViewBag.Cinemas = new SelectList(movieDropdowns.Cinemas, "Id", "Name");
        ViewBag.Producers = new SelectList(movieDropdowns.Producers, "Id", "FullName");
        ViewBag.Actors = new SelectList(movieDropdowns.Actors, "Id", "FullName");

        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(MovieVM movie)
    {
        if (movie.Id == 0)
        {
            return View("_NotFound");
        }
        if (!ModelState.IsValid)
        {
            return View(movie);
        }

        await _service.UpdateMovieAsync(movie);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Filter(string searchString)
    {
        var allMovies = await _service.GetAllAsync(m => m.Cinema);
        if (!string.IsNullOrEmpty(searchString.ToLower()))
        {
            var filteredResult = allMovies.Where(m => m.Name.ToLower().Contains(searchString)
                                                || m.Description.ToLower().Contains(searchString))
                                           .ToList();

            if (filteredResult.Any())
            {
                return View("Index", filteredResult);
            }
            else
            {
                return View("Index", allMovies);
            }


        }

        return View("_NotFound");
        
    }
}
