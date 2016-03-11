using Transverse.Models.DAL;

namespace Transverse.Interfaces.DAL
{
    public interface IUserRepository : IRepository<User>
    {
        User GetById(int id);
        User GetByEmail(string email);
    }
}