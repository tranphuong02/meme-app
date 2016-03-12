using System.Collections.Generic;
using System.Web.Mvc;

namespace Web.Areas.Administrator.Controllers.WEB
{
    [AllowAnonymous]
    public class EmailController : BaseController
    {
        public ActionResult EmailForgotPassword(string email, string token)
        {
            var link = $"{Request.Url.Scheme}://{Request.Url.Authority}/Account/ResetPassword?email={email}&token={token}";

            return View(new KeyValuePair<string, string>(email, link));
        }

        public ActionResult EmailAdminResetPasswordForUser(string email,  string token)
        {
            var link = $"{Request.Url.Scheme}://{Request.Url.Authority}/Account/ResetPassword?email={email}&token={token}";

            return this.View(new KeyValuePair<string, string>(email, link));
        }
    }
}