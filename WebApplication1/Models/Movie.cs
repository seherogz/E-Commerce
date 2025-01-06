
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Data.Base;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Models;

public class Movie : IEntityBase
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public MovieCategory MovieCategory { get; set; }

    //Relationships
    [ValidateNever]
    public List<Actor_Movie> Actors_Movies { get; set; }


    [ForeignKey("CinemaId")]
    [ValidateNever]
    public int CinemaId { get; set; }


    [ValidateNever]
    public Cinema Cinema { get; set; }


    [ForeignKey("ProducerId")]
    [ValidateNever]
    public int ProducerId { get; set; }


    [ValidateNever]
    public Producer Producer { get; set; }
}