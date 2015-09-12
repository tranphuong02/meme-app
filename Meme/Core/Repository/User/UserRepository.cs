using Core.UnitOfWork;

namespace Core.Repository.User
{
    public class UserRepository : Common.Repository<Model.Entities.User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
