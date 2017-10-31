using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels.Authentificate
{
    public class ForgotViewModel
    {
        [Microsoft.Build.Framework.Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
