using System.Collections.Generic;
using Framework.DI.Contracts.Interfaces;
using Transverse.Models.DAL;

namespace Transverse.Interfaces.Business
{
    public interface IRoleBusiness : IDependency
    {
        IList<Role> GetAll();
    }
}