using System.Linq;
using System.Web.Mvc;

namespace Web.Areas.Administrator.Controllers.WEB
{
    [Authorize]
    public class BaseController : Controller
    {
        protected bool IsAuthenticate => User.Identity.IsAuthenticated;

        protected string GetModelErrorMessage()
        {
            var message = string.Empty;
            message = ModelState.Values.Aggregate(message, (current1, modelState) => modelState.Errors.Aggregate(current1, (current, error) => current + (error.ErrorMessage + @"<br/>")));
            message = message.Trim();
            return message;
        }
    }
}