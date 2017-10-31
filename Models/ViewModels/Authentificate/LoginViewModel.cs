using System.ComponentModel.DataAnnotations;
using FuckThisNumber.Interfaces;

namespace Models.ViewModels.Authentificate
{
    public class LoginViewModel : ILoginViewModel
    {
        [Microsoft.Build.Framework.Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Microsoft.Build.Framework.Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}