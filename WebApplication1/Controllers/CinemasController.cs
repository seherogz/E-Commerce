using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Data;
using WebApplication1.Data.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class CinemasController : Controller
{
    private readonly ICinemasService _service;
    private object cinemaDetail;

    public CinemasController(ICinemasService service)

    {

        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var allCinemas = await _service.GetAllAsync();
        return View(allCinemas);
    }


    public async Task<IActionResult> Create() //önce boş bir view alıyorum sonra onu doldurup yoluyorum. Yani en başta bu işlemle boş sayfa açmış oluyorum.
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Cinema cinema) //burda da bu boş sayfayı dolduruyorum.
    {
        if (!ModelState.IsValid)
        {
            return View(cinema);
        }
        await _service.AddAsync(cinema);
        return RedirectToAction(nameof(Index));

    }

    public async Task<IActionResult> Detail(int id)
    {
        var cinemaDetail = await _service.GetByIdAsync(id); //güncelleme yaparken neyi güncelleme yapacağımı bulmak için id'sini alırım.
        if (cinemaDetail.Id == 0)
        {
            return View("_NotFound");
        }
        return View(cinemaDetail);
    }


    public async Task<IActionResult> EditAsync(int id) //burda get işlemi var. yazmasak bile [HttpGet] yazıyor.
    {
        var cinemaDetail = await _service.GetByIdAsync(id); //burda id'si ile datadan veriyi buluyor.
        if (cinemaDetail.Id == 0)
        {
            return View("_NotFound");
        }
        return View(cinemaDetail);

    }


    [HttpPost]
    public async Task<IActionResult> EditAsync(Cinema cinema)
    {
        if (!ModelState.IsValid)
        {
            return View(cinema);
        }
        await _service.UpdateAsync(cinema);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)  //bu görüntülemek için
    {
        var cinemaDetail = await _service.GetByIdAsync(id);
        if (cinemaDetail.Id == 0)
        {
            return View("_NotFound");
        }
        return View(cinemaDetail);

    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id) //burda da emin olup olmadığını soracağız.
    {

        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
