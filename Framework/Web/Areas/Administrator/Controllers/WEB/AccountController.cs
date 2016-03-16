using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Framework.Utility;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Transverse.Interfaces.Business;
using Transverse.Models.Business.Account;
using Transverse.Models.DAL;
using Transverse.Utils;
using Constants = Transverse.Constants;

namespace Web.Areas.Administrator.Controllers.WEB
{
    public class AccountController : BaseController
    {
        [Dependency]
        public IUserBusiness UserBusiness { get; set; }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (IsAuthenticate)
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null)
            {
                returnUrl = Server.UrlEncode(Request.UrlReferrer.PathAndQuery);
            }

            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnURL = returnUrl;
            }

            var loginViewModel = new LoginViewModel();

            return View(loginViewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel viewModel, string returnUrl)
        {
            if (IsAuthenticate)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var validateResult = UserBusiness.Authenticate(viewModel);

            if (!validateResult.IsSuccess)
            {
                ModelState.AddModelError("", validateResult.Message);
                return View(viewModel);
            }

            var user = (User)validateResult.Data;
            var userData = JsonConvert.SerializeObject(new PrincipalSerializeViewModel(user));
            var authTicket = new FormsAuthenticationTicket(
                1,
                user.Email,
                DateTimeHelper.UTCNow(),
                DateTimeHelper.UTCNow().AddDays(BackendHelpers.FormsAuthenticationCookieTimeout()),
                viewModel.RememberMe,
                userData);

            var encTicket = FormsAuthentication.Encrypt(authTicket);
            var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);

            //returnURL needs to be decoded
            var decodedUrl = string.Empty;

            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                decodedUrl = Server.UrlDecode(returnUrl);
            }

            if (Url.IsLocalUrl(decodedUrl))
            {
                return Redirect(decodedUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.User = null;
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var result = UserBusiness.GetDataForgotPassword(viewModel.Email);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                return View(viewModel);
            }

            try
            {
                var model = (SendMailInfoVM)result.Data;
                var newRequest = $"{Request.Url.Scheme}://{Request.Url.Authority}/Email/EmailForgotPassword?email={model.Email}&token={model.Token}";
                var body = new WebClient().DownloadString(newRequest);

                var emailHelper = new ConfigEmailHelpers().NewEmail();
                emailHelper.Body = body;
                emailHelper.Recipient = model.Email;
                emailHelper.Subject = $"Your login details for {Constants.AppName}";
                emailHelper.Send();
            }
            catch (Exception ex)
            {
                Framework.Logger.Log4Net.Provider.Instance.LogError(ex);
                result.Message = $"{result.Message} But, sending email failed.";
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return RedirectToAction("UrlInvalid", "Error");
            }

            var result = UserBusiness.ValidateToken(email, token);

            if (!result.IsSuccess)
            {
                return RedirectToAction("UrlInvalid", "Error");
            }

            var viewModel = new ResetPasswordViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string email, string token, ResetPasswordViewModel viewModel)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return RedirectToAction("UrlInvalid", "Error");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var result = UserBusiness.ChangePassword(email, token, viewModel.NewPassword);

            if (result.IsSuccess)
            {
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", result.Message);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (User.IsInRole(Constants.RoleName.SuperAdmin))
            {
                return HttpNotFound();
            }
            var viewModel = new ChangePasswordViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (User.IsInRole(Constants.RoleName.SuperAdmin))
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                TempData.SetStatusMessage(GetModelErrorMessage(), UtilityEnum.StatusMessageType.Danger);
                return View(viewModel);
            }

            var result = UserBusiness.ChangePassword(viewModel, BackendHelpers.CurrentUserId());
            if (!result.IsSuccess)
            {
                TempData.SetStatusMessage(result.Message, UtilityEnum.StatusMessageType.Danger);
                return View(viewModel);
            }

            TempData.SetStatusMessage(result.Message);
            return RedirectToAction("Index", "Home");
        }
    }
}