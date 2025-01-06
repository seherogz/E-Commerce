using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Data;
using WebApplication1.Data.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers;
public class ProducersController : Controller
{
    private readonly IProducersService _service;
    private object producerDetail;

    public ProducersController(IProducersService service)

    {

        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var allProducers = await _service.GetAllAsync(); //var değişkenimin tipini list olarak tanımladı. Tolist'i silseydim de data tipim Producers olacaktı
        return View(allProducers);
    }


    [HttpGet]
    public async Task<IActionResult> Create() //önce boş bir view alıyorum sonra onu doldurup yoluyorum. Yani en başta bu işlemle boş sayfa açmış oluyorum.
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Producer producer) //burda da bu boş sayfayı dolduruyorum.
    {
        if (!ModelState.IsValid)
        {
            return View(producer);
        }
        await _service.AddAsync(producer);
        return RedirectToAction(nameof(Index));

    }

    public async Task<IActionResult> Detail(int id)
    {
        var producerDetail = await _service.GetByIdAsync(id); //güncelleme yaparken neyi güncelleme yapacağımı bulmak için id'sini alırım.
        if (producerDetail.Id == 0)
        {
            return View("_NotFound");
        }
        return View(producerDetail);
    }


    public async Task<IActionResult> EditAsync(int id) //burda get işlemi var. yazmasak bile [HttpGet] yazıyor.
    {
        var producerDetail = await _service.GetByIdAsync(id); //burda id'si ile datadan veriyi buluyor.
        if (producerDetail.Id == 0)
        {
            return View("_NotFound");
        }
        return View(producerDetail);

    }


    [HttpPost]
    public async Task<IActionResult> EditAsync(Producer producer)
    {
        if (!ModelState.IsValid)
        {
            return View(producer);
        }
        await _service.UpdateAsync(producer);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)  //bu görüntülemek için
    {
        var producerDetail = await _service.GetByIdAsync(id);
        if (producerDetail.Id == 0)
        {
            return View("_NotFound");
        }
        return View(producerDetail);

    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id) //burda da emin olup olmadığını soracağız.
    {

        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
