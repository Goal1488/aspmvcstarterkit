using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels.Authentificate
{
    public class ExternalLoginConfirmationViewModel
    {
        [Microsoft.Build.Framework.Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}