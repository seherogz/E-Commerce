using System.ComponentModel.DataAnnotations;
using WebApplication1.Data.Base;

namespace WebApplication1.Models
{
    public class Producer : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profil Resmi")]
        [Required(ErrorMessage = "Profil resmi zorunludur")]
        public string ProfilePictureUrl { get; set; } = string.Empty;

        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage = "Ad soyad  zorunludur")] //bu kısım kesinlikle boş geçilmemeli.
        [StringLength(maximumLength: 50, ErrorMessage = "İsim 3 ile 50 karakter arasında olmalıdır.", MinimumLength = 3)] //bu da yapılması gerekn diğer kural.
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Biografi")]
        [Required(ErrorMessage = "Biografi zorunludur")]
        public string Bio { get; set; } = string.Empty;
       

    }
}