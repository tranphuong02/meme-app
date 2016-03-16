using System.Web.Mvc;

namespace Web.Areas.Administrator.Controllers.API
{
    public class HomeApiController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}