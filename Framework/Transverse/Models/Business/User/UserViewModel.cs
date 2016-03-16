using Transverse.Attributes;
using Transverse.Enums;

namespace Transverse.Models.Business.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }

        [EnumConverterFromStringToInt(typeof(ActiveType))]
        public bool IsActive { get; set; }
    }
}
