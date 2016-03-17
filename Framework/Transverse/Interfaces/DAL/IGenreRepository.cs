using Transverse.Models.DAL;
using BaseModel = Transverse.Models.Business.BaseModel;

namespace Transverse.Interfaces.DAL
{
    public interface IGenreRepository : IRepository<Genre>
    {
        BaseModel GetAll();
    }
}