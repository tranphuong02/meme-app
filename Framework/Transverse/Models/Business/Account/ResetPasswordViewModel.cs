using System.ComponentModel.DataAnnotations;

namespace Transverse.Models.Business.Account
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = @"New password is required")]
        [MinLength(6, ErrorMessage = @"Passwords must be 6–15 characters in length")]
        [MaxLength(15, ErrorMessage = @"Passwords must be 6–15 characters in length")]
        [DataType(DataType.Password)]
        [Display(Name = @"New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = @"Confirm password is required")]
        [Compare("NewPassword", ErrorMessage = @"New password and confirm password doesnot match")]
        [DataType(DataType.Password)]
        [Display(Name = @"Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}