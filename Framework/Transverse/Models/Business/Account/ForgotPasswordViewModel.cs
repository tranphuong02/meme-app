using System.ComponentModel.DataAnnotations;

namespace Transverse.Models.Business.Account
{
    public class ForgotPasswordViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = @"Email is required")]
        [EmailAddress]
        public string Email { get; set; }
    }
}