using Transverse.Models.DAL;
using BaseModel = Transverse.Models.Business.BaseModel;

namespace Transverse.Interfaces.DAL
{
    public interface IAuthorRepository : IRepository<Author>
    {
        BaseModel GetAll();
    }
}