using System.Web.Mvc;

namespace Web.Areas.Administrator.Controllers.WEB
{
    [AllowAnonymous]
    public class ErrorController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TooBigFile()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UrlInvalid()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PageNotFound()
        {
            return View();
        }
    }
}