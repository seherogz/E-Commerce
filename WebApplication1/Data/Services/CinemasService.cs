using WebApplication1.Data.Base;
using WebApplication1.Models;

namespace WebApplication1.Data.Services;

public class CinemasService : EntityBaseRepository<Cinema>, ICinemasService
{
    readonly AppDbContext _context;

    public CinemasService(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
