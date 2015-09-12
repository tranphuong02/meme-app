using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Constants;
using FluentValidation;
using FluentValidation.Attributes;

namespace Core.ViewModel.User
{
    [Validator(typeof(UserViewValidatior))]
    public class UserViewModel : BaseViewModel
    {
        [Display(Name = @"Username")]
        public string UserName { get; set; }

        [Display(Name = @"Password")]
        public string Password { get; set; }

        [Display(Name = @"Salt Key")]
        public string SaltKey { get; set; }

        [Display(Name = @"First Name")]
        public string FirstName { get; set; }

        [Display(Name = @"Last Name")]
        public string LastName { get; set; }

        [Display(Name = @"Role")]
        public int Role { get; set; }
    }

    public class UserViewValidatior : AbstractValidator<UserViewModel>
    {
        public UserViewValidatior()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .Must(x => !x.All(char.IsWhiteSpace))
                .Must(x => !x.All(char.IsSymbol))
                .Length(AppConstants.MinText, AppConstants.ShortText)
                .Must(UniqueUserName);

            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(1, AppConstants.ShortText);

            RuleFor(x => x.SaltKey)
                .NotEmpty()
                .Length(1, AppConstants.ShortText);

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(AppConstants.MinText, AppConstants.ShortText);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(AppConstants.MinText, AppConstants.ShortText);
        }


        private bool UniqueUserName(string username)
        {
            return true;
        }
    }
}
