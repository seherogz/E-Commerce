using System.ComponentModel.DataAnnotations;
using WebApplication1.Data.Base;

namespace WebApplication1.Models;

public class ShoppingCartItem:IEntityBase
{
    [Key]
    public int Id { get; set; }
    public Movie Movie { get; set; }
    public int Amount { get; set; }
    public String ShoppingCardId { get; set; }

}
