using WebApplication1.Models;

namespace WebApplication1.Data.Services
{
  
 
    public interface IActorsService
    {
        Task<IEnumerable<Actor>> GetAllAsync();

        Task<Actor> GetByIdAsync(int id);

        Task AddAsync(Actor actor);

        Task<Actor> UpdateAsync(Actor newActor);

        Task DeleteAsync(int id);
    }
}
