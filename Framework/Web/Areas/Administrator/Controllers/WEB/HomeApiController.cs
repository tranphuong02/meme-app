using System.Web.Mvc;

namespace Web.Areas.Administrator.Controllers.WEB
{
    public class HomeApiController : Controller
    {
        /// <summary>
        /// Redirect to swagger
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}