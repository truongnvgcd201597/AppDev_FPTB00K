using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace FPTBook.Models;

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [StringLength(50)]
    public string Fullname { get; set; }

    [PersonalData]
    [StringLength(250)]
    public string HomeAddress { get; set; }
}
