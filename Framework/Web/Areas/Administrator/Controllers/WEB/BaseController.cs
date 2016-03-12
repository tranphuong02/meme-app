using System.Web.Mvc;
namespace Web.Areas.Administrator.Controllers.WEB
{
    [Authorize]
    public class BaseController : Controller
    {
        protected bool IsAuthenticate => User.Identity.IsAuthenticated;
    }
}