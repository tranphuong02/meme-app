using System.ComponentModel;

namespace Transverse.Enums
{
    public enum RoleEnum
    {
        [Description("Supper Administrator")]
        SuperAdmin = 1,

        [Description("Administrator")]
        Admin = 2,

        [Description("Moderator")]
        Mod = 3,

        [Description("Member")]
        Member = 4
    }

    public class RoleName
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string Mod = "Mod";
        public const string Member = "Member";
    }
}