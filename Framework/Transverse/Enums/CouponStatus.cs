using System.ComponentModel.DataAnnotations;

namespace Transverse.Enums
{
    public enum CouponStatus
    {
        [Display(Name = @"Normal")]
        Normal = 1,

        [Display(Name = @"Hot")]
        Hot = 2,

        [Display(Name = @"New")]
        New = 3,
    }
}