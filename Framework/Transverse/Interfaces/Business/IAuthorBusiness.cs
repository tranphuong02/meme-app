using Framework.DI.Contracts.Interfaces;
using Transverse.Models.Business;

namespace Transverse.Interfaces.Business
{
    public interface IAuthorBusiness : IDependency
    {
        BaseModel GetAll();
    }
}