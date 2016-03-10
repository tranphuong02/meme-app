using System.Web.Mvc;

namespace Web.Areas.Administrator.Controllers.WEB
{
    public class HomeController : Controller
    {
        //
        // GET: /Administrator/Home/
        public ActionResult Index()
        {
            return View();
        }
    }
}