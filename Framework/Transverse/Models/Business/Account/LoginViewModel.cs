using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Transverse.Models.Business.Account
{
    public class LoginViewModel
    {
        [DisplayName(@"Email")]
        [Required( ErrorMessage = @"Email is required")]
        [RegularExpression(@"[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}",ErrorMessage = @"Invalid email address")]
        public string Email { get; set; }

        [DisplayName(@"Password")]
        [Required(ErrorMessage = @"Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = @"Remember me?")]
        public bool RememberMe { get; set; }
    }
}
