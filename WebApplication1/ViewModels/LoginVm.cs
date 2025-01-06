using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels;

public class LoginVm
{

    [Required(ErrorMessage = "E-mail adresi gereklidir.")]
    [Display(Name = "E-mail adresi")]
    public string EmailAdress { get; set; }

    [Required(ErrorMessage = "Şifre gereklidir.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Password { get; set; }
}
