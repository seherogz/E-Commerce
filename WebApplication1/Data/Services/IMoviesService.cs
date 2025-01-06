using WebApplication1.Data.Base;
using WebApplication1.Models;
using WebApplication1.ViewModels;


namespace WebApplication1.Data.Services;

public interface IMoviesService : IEntityBaseRepository<Movie>
{
    Task<Movie> GetMovieByIdAsync(int id);

    Task<MovieDropdowns> GetMovieDropdownsValuesAsync();

    Task AddMovieAsync(MovieVM movie);

    Task UpdateMovieAsync(MovieVM movie);
}
