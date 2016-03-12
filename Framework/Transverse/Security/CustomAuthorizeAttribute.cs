using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Transverse.Models.Business.Account;

namespace Transverse.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        protected virtual PrincipalViewModel CurrentUser => HttpContext.Current.User as PrincipalViewModel;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(Roles))
                {
                    if (!CurrentUser.IsInRole(Roles))
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login", area = Constants.AdminArea }));
                    }
                }

                if (!string.IsNullOrEmpty(Users))
                {
                    if (!Users.Contains(CurrentUser.Id.ToString()))
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login", area = Constants.AdminArea }));
                    }
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login", area = Constants.AdminArea }));
            }
        }
    }
}
