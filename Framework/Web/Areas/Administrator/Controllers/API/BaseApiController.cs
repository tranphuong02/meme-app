using System.Web.Http;
using Transverse.Security;

namespace Web.Areas.Administrator.Controllers.API
{
    [CustomAuthorize]
    public class BaseApiController : ApiController
    {
    }
}