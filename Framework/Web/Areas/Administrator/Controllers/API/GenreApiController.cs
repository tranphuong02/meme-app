using System.Web.Http;
using Microsoft.Practices.Unity;
using Transverse.Interfaces.Business;

namespace Web.Areas.Administrator.Controllers.API
{
    [RoutePrefix("api/GenreApi")]
    public class GenreApiController : BaseApiController
    {
        [Dependency]
        public IGenreBusiness GenreBusiness { get; set; }

        [HttpPost]
        public IHttpActionResult GetAll()
        {
            var result = GenreBusiness.GetAll();
            return Ok(result);
        }
    }
}