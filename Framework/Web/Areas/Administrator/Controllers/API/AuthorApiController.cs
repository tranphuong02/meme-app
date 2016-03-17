using Microsoft.Practices.Unity;
using System.Web.Http;
using Transverse.Interfaces.Business;

namespace Web.Areas.Administrator.Controllers.API
{
    [RoutePrefix("api/AuthorApi")]
    public class AuthorApiController : BaseApiController
    {
        [Dependency]
        public IAuthorBusiness AuthorBusiness { get; set; }

        [HttpPost]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            var result = AuthorBusiness.GetAll();

            return Ok(result);
        }
    }
}