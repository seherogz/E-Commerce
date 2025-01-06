using WebApplication1.Data;
using System.ComponentModel.DataAnnotations;


namespace WebApplication1.Models;

public class MovieVM
{
    public int Id { get; set; }

    [Display(Name = "Film Adı")]
    [Required(ErrorMessage = "Film adı gereklidir")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Film tanıtımı")]
    [Required(ErrorMessage = "Film tanıtımı gereklidir")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Bilet fiyatı")]
    [Required(ErrorMessage = "Bilet fiyatı gereklidir")]
    public double Price { get; set; }

    [Display(Name = "Film Afişi")]
    [Required(ErrorMessage = "Film afişi gereklidir")]
    public string ImageUrl { get; set; } = string.Empty;

    [Display(Name = "Film gösterim başlangıç tarihi")]
    [Required(ErrorMessage = "Film gösterim başlangıç tarihi gereklidir")]
    public DateTime StartDate { get; set; }

    [Display(Name = "Film gösterim bitiş tarihi")]
    [Required(ErrorMessage = "Film gösterim bitiş tarihi gereklidir")]
    public DateTime EndDate { get; set; }

    [Display(Name = "Film Türü")]
    [Required(ErrorMessage = "Film türü gereklidir")]
    public MovieCategory MovieCategory { get; set; }

    [Display(Name = "Aktör seçiniz")]
    [Required(ErrorMessage = "Aktör gereklidir")]
    public List<int> ActorIds { get; set; }


    [Display(Name = "Sinema Seçiniz")]
    [Required(ErrorMessage = "Sinema bilgisi gereklidir")]
    public int CinemaId { get; set; }


    [Display(Name = "Produktör seçiniz")]
    [Required(ErrorMessage = "Produktör bilgisi gereklidir")]
    public int ProducerId { get; set; }


}
