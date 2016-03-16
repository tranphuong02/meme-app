using System.Linq;
using System.Security.Principal;

namespace Transverse.Models.Business.Account
{
    public class PrincipalViewModel : IPrincipal
    {
        public PrincipalViewModel(string username)
        {
            Identity = new GenericIdentity(username);
        }

        public IIdentity Identity { get; private set; }

        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public bool IsInRole(string role)
        {
            if (string.IsNullOrWhiteSpace(Role))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(role))
            {
                return true;
            }

            var roles = role.Split(',').ToList();
            return roles.Any(x => Role.Trim().ToLower().Contains(x.Trim().ToLower()));
        }
     }
}
