using Microsoft.Practices.Unity;
using Transverse.Interfaces.Business;
using Transverse.Interfaces.DAL;
using Transverse.Models.Business;

namespace BusinessLogic
{
    public class AuthorBusiness : IAuthorBusiness
    {
        [Dependency]
        public IAuthorRepository AuthorRepository { get; set; }

        public BaseModel GetAll()
        {
            return AuthorRepository.GetAll();
        }
    }
}