using Framework.DI.Unity;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Transverse.Interfaces.Business;
using Transverse.Models.DAL;

namespace Transverse.Models.Business.User
{
    public class UserAddViewModel
    {
        [DisplayName(@"Email")]
        [Required(ErrorMessage = @"Email is required")]
        [RegularExpression(@"[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}", ErrorMessage = @"Invalid email address")]
        public string Email { get; set; }

        [DisplayName(@"First Name")]
        [Required(ErrorMessage = @"First name is required")]
        public string FirstName { get; set; }

        [DisplayName(@"Last Name")]
        [Required(ErrorMessage = @"Last name is required")]
        public string LastName { get; set; }

        [DisplayName(@"Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = @"Role")]
        [Required(ErrorMessage = @"Please choose a role")]
        public int RoleId { get; set; }

        public IList<Role> Roles { get; set; }

        public void Init()
        {
            var userBusiness = IoCFactory.Instance.GetObjectInstance<IUserBusiness>();
            userBusiness.InitAddViewModel(this);
        }
    }
}