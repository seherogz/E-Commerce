using System.ComponentModel.DataAnnotations;
using WebApplication1.Data.Base;

namespace WebApplication1.Models
{
    public class Order : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }

        public List<OrderItem> OderItem { get; set; }
    }

}
