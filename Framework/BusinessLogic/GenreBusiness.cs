using Microsoft.Practices.Unity;
using Transverse.Interfaces.Business;
using Transverse.Interfaces.DAL;
using Transverse.Models.Business;

namespace BusinessLogic
{
    public class GenreBusiness : IGenreBusiness
    {
        [Dependency]
        public IGenreRepository GenreRepository { get; set; }

        public BaseModel GetAll()
        {
            return GenreRepository.GetAll();
        }
    }
}