using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profil Resmi")]
        public string ProfilePictureUrl { get; set; } = string.Empty;

        [Display(Name = "Ad Soyad")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Biografi")]
        public string Bio { get; set; } = string.Empty;
       

    }
}