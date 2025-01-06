using WebApplication1.Data.Base;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Data.Services;

public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
{
    readonly AppDbContext _context;

    public MoviesService(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Movie> GetMovieByIdAsync(int id)
    {
        var movieDetails = await _context.Movies
            .Include(c => c.Cinema)
            .Include(p => p.Producer)
            .Include(m => m.Actors_Movies).ThenInclude(a => a.Actor)
            .FirstOrDefaultAsync(n => n.Id == id);

        return movieDetails;
    }

    public async Task<MovieDropdowns> GetMovieDropdownsValuesAsync()
    {
        var response = new MovieDropdowns()
        {
            Actors = await _context.Actors.OrderBy(c => c.FullName).ToListAsync(),
            Cinemas = await _context.Cinemas.OrderBy(c => c.Name).ToListAsync(),
            Producers = await _context.Producers.OrderBy(c => c.FullName).ToListAsync()
        };

        return response;
    }

    public async Task AddMovieAsync(MovieVM movie)
    {
        var newMovie = new Movie()
        {
            CinemaId = movie.CinemaId,
            Description = movie.Description,
            EndDate = movie.EndDate,
            ImageUrl = movie.ImageUrl,
            MovieCategory = movie.MovieCategory,
            Name = movie.Name,
            Price = movie.Price,
            ProducerId = movie.ProducerId,
            StartDate = movie.StartDate
        };

        await _context.Movies.AddAsync(newMovie);
        await _context.SaveChangesAsync();

        foreach (var actorId in movie.ActorIds)
        {
            var newActorMovie = new Actor_Movie
            {
                ActorId = actorId,
                MovieId = newMovie.Id
            };

            await _context.Actors_Movies.AddAsync(newActorMovie);
        }

        await _context.SaveChangesAsync();

    }

    public async Task UpdateMovieAsync(MovieVM movie)
    {
        var existingMovie = _context.Movies.FirstOrDefault(m => m.Id == movie.Id);

        if (existingMovie != null)
        {

            existingMovie.CinemaId = movie.CinemaId;
            existingMovie.Description = movie.Description;
            existingMovie.EndDate = movie.EndDate;
            existingMovie.ImageUrl = movie.ImageUrl;
            existingMovie.MovieCategory = movie.MovieCategory;
            existingMovie.Name = movie.Name;
            existingMovie.Price = movie.Price;
            existingMovie.ProducerId = movie.ProducerId;
            existingMovie.StartDate = movie.StartDate;
            await _context.SaveChangesAsync();

            //remove old actor ıds

            var existingActors= _context.Actors_Movies.Where(a => a.MovieId == movie.Id).ToList();

            _context.Actors_Movies.RemoveRange(existingActors);
            await _context.SaveChangesAsync();



            foreach (var actorId in movie.ActorIds)
            {
                var newActorMovie = new Actor_Movie
                {
                    ActorId = actorId,
                    MovieId = movie.Id
                };

                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
        }


            await _context.SaveChangesAsync();
    }

    
    
}
