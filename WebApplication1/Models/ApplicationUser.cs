using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class ApplicationUser : IdentityUser
    //reflection: marker interface. Sadecee sınıflandırmak için kullandığım interfaceler için kullanılır. O interface ile onu implement eden classı işaretliyor.
    //interfaceler hiçbir işlem yapılmadan sadece marker interface olarak da tanımlanabilir. Classalrın hangi interface'yi implement ettiğini işaretliyor.
    
{
    [Display(Name = "User Name")]
    public string FullName { get; set; } = string.Empty;



}
