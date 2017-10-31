using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels.Authentificate
{
    public class ForgotPasswordViewModel
    {
        [Microsoft.Build.Framework.Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}