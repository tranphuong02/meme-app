using System.Web.Mvc;

namespace App.Areas.Administrator.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
    }
}