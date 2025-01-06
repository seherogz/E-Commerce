using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels;

public class RegisterVm
{
    [Required(ErrorMessage ="Ad soyad gereklidir.")]
    [Display(Name = "Ad Soyad")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "E-mail adresi gereklidir.")]
    [Display(Name = "E-mail adresi")]
    public string EmailAddress { get; set; }

    [Required(ErrorMessage = "Şifre gereklidir.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Şifre tekrarı gereklidir.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    [Compare("Password",ErrorMessage = "Şifreler uyuşmuyor")]
    public string ConfirmePassword { get; set; }
}
