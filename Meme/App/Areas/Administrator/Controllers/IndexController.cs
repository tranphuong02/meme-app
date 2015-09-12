using System.Web.Mvc;

namespace App.Areas.Administrator.Controllers
{
    public class IndexController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}