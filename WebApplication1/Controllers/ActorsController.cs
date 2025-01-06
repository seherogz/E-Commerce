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


    public async Task<IActionResult> Create() //önce boş bir view alıyorum sonra onu doldurup yoluyorum. Yani en başta bu işlemle boş sayfa açmış oluyorum.
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Actor actor) //burda da bu boş sayfayı dolduruyorum.
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
        var actorDetail = await _service.GetByIdAsync(id); //güncelleme yaparken neyi güncelleme yapacağımı bulmak için id'sini alırım.
        if (actorDetail.Id == 0)
        {
            return View("_NotFound");
        }
        return View(actorDetail);
    }


    public async Task<IActionResult> EditAsync(int id) //burda get işlemi var. yazmasak bile [HttpGet] yazıyor.
    {
        var actorDetail = await _service.GetByIdAsync(id); //burda id'si ile datadan veriyi buluyor.
        if (actorDetail.Id == 0)
        {
            return View("_NotFound");
        }
        return View(actorDetail);
    
    }


    [HttpPost]
    public async Task<IActionResult> EditAsync(Actor actor) 
    {
        if (!ModelState.IsValid)
        {
            return View(actor);
        }
        await _service.UpdateAsync(actor);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)  //bu görüntülemek için
    {
        var actorDetail = await _service.GetByIdAsync(id); 
        if (actorDetail.Id == 0)
        {
            return View("_NotFound");
        }
        return View(actorDetail);

    }

    [HttpPost,ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id) //burda da emin olup olmadığını soracağız.
    {
  
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
