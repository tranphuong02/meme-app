using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transverse.Models.DAL
{
    public class User : BaseModel
    {
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsActive { get; set; }
        public string Token { get; set; }

        // Helper Properties
        public virtual Role Role { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}