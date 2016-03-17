using Framework.DI.Contracts.Interfaces;
using Transverse.Models.Business;

namespace Transverse.Interfaces.Business
{
    public interface IGenreBusiness : IDependency
    {
        BaseModel GetAll();
    }
}