using System.ComponentModel.DataAnnotations;
using Common.Constants;

namespace Core.Model.Entities
{
    public class User : BaseEntity
    {
        [StringLength(AppConstants.ShortText)]
        [Required]
        public string UserName { get; set; }

        [StringLength(AppConstants.ShortText)]
        [Required]
        public string Password { get; set; }

        [StringLength(AppConstants.ShortText)]
        [Required]
        public string SaltKey { get; set; }

        [StringLength(AppConstants.ShortText)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(AppConstants.ShortText)]
        [Required]
        public string LastName { get; set; }

        public int Role { get; set; }
    }
}
