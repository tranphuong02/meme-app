using System.Collections.Generic;
using Transverse.Models.DAL;

namespace Transverse.Interfaces.DAL
{
    public interface IRoleRepository : IRepository<Role>
    {
        IList<Role> GetAll();
    }
}