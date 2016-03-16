using System.ComponentModel.DataAnnotations;
using Framework.DI.Unity;
using Transverse.Interfaces.Business;

namespace Transverse.Models.Business.Account
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = @"New password is required")]
        [MinLength(6, ErrorMessage = @"New password must be 6–15 characters in length")]
        [MaxLength(15, ErrorMessage = @"New password must be 6–15 characters in length")]
        [DataType(DataType.Password)]
        [Display(Name = @"New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = @"Confirm password is required")]
        [Compare("NewPassword", ErrorMessage = @"Confirm password and new password does not match")]
        [DataType(DataType.Password)]
        [Display(Name = @"Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}