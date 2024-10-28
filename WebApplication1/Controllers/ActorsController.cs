using WebApplication1.Data;
using WebApplication1.Data.Services;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ActorsController : Controller
{
    private readonly IActorsService _service;
    private object actordetail;

    public ActorsController(IActorsService service)

    {

        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var allActors = await _service.GetAllAsync();
        return View(allActors);
    }


    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Actor actor)
    {
        if (!ModelState.IsValid)
        {
            return View(actor);
        }
        await _service.AddAsync(actor);
        return RedirectToAction(nameof(Index));

    }

    public async Task<IActionResult> Detail(int id)
    {
        var actorDetail = await _service.GetByIdAsync(id);
        if (actorDetail is null)
        {
            return View("Empty");
        }
        return View(actorDetail);
    }


    public async Task<IActionResult> EditAsync()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> EditAsync(Actor actor) // burası odev olarak verıldı edit kısmı olacak 
    {
        if (!ModelState.IsValid)
        {
            return View(actor);
        }
        await _service.UpdateAsync(actor);
        return RedirectToAction(nameof(Index));
    }
void metod()
{
   
}
}
