using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels.Authentificate
{
    public class VerifyCodeViewModel
    {
        [Microsoft.Build.Framework.Required]
        public string Provider { get; set; }

        [Microsoft.Build.Framework.Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }
}