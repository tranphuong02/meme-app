using Transverse.Models.DAL;

namespace Transverse.Models.Business.Account
{
    public class PrincipalSerializeViewModel
    {
        public PrincipalSerializeViewModel()
        {
            
        }
        public PrincipalSerializeViewModel(User user)
        {
            Id = user.Id;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Role = user.Role.Name;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}