using System.ComponentModel.DataAnnotations;

namespace TeamUp.Core.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
