using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Cinema
{

    [Key]

    public int Id { get; set; }

    [Display(Name = "Logo")]
    public string Logo { get; set; } = string.Empty;

    [Display(Name = "Cinema Salonu")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Açıklama")]
    public string Description { get; set; } = string.Empty;

    //RelationShips

    [ValidateNever]
    public List<Movie> Movies { get; set; } //bir sinemanın yayınlayacağı birden çok film vardır.

}
