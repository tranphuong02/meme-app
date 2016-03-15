using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;
using Transverse.Interfaces.Business;
using Transverse.Interfaces.DAL;
using Transverse.Models.DAL;

namespace BusinessLogic
{
    public class RoleBusiness : IRoleBusiness
    {
        [Dependency]
        public IRoleRepository RoleRepository { get; set; }

        
    }
}