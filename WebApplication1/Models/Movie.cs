using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Data;

namespace WebApplication1.Models;

public class Movie
{

    [Key]
    public int Id { get; set; } 

    [Display(Name = "Filmin Resmi")]
    public string ImageUrl { get; set; } = string.Empty;

    [Display(Name = "Filmin Adı")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Açıklama")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Tür")]
    public MovieCategory MovieCategory { get; set; }

    [Display(Name = "Başlangıç Tarihi")]
    public DateTime StartDate { get; set; }

    [Display(Name = "Bitiş Tarihi")]
    public DateTime EndDate { get; set; }
    
    [Display(Name = "Ücret")]
    public double Price { get; set; }


    //RELATIONSHIP

    [ValidateNever]
    public List<Actor_Movie> Actors_Movies { get; set; }

    [ForeignKey("CinemaId")]
    [ValidateNever]
    public int CinemaId { get; set; } //Cinema ile movie'yi birbirine bağlayan foreign key.

    [ValidateNever]
    public Cinema Cinema { get; set; } //Bir film bir sinema salonunda sergilenecek projemizde.

    [ForeignKey("ProducerId)")] //bu foreign key olmasını sağlıyor. Producer tablosundaki bir pk movie tablosunda kullanılıyorsa bu fk oluyor.
    [ValidateNever]
    public int ProducerId { get; set; }

    [ValidateNever]
    public Producer Producer { get; set; }
}

