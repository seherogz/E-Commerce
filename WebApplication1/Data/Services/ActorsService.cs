using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Services;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Data.Base;

namespace WebApplication1.Data.Services;

public class ActorsService :EntityBaseRepository<Actor>, IActorsService//bu class dbcontext e bağımlı
{
    readonly AppDbContext _context;

    public ActorsService(AppDbContext context) : base(context)//dependency inversion
        //burda kullandığım base miras aldığım class yani EntityBaseRepository.
        //base class parant class ile iletişim için kullanıyoruz.
    {
        _context = context;
    }
}