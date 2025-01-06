using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Services;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Data.Base;

namespace WebApplication1.Data.Services;

public class ProducersService : EntityBaseRepository<Producer>, IProducersService
{
    readonly AppDbContext _context;

    public ProducersService(AppDbContext context) : base(context)
    {
        _context = context;
    }
}