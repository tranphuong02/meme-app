using System.Web.Mvc;

namespace Web.Areas.Administrator.Controllers.WEB
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}