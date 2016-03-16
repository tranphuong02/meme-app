using Framework.Datatable.RequestBinder;
using Framework.Utility;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Transverse.Interfaces.Business;
using Transverse.Models.Business.Account;
using Transverse.Models.Business.User;
using Transverse.Security;
using Constants = Transverse.Constants;

namespace Web.Areas.Administrator.Controllers.WEB
{
    [CustomAuthorize(Roles = Constants.RoleName.SuperAdmin)]
    public class UserController : BaseController
    {
        [Dependency]
        public IUserBusiness UserBusiness { get; set; }

        [Dependency]
        public IRoleBusiness RoleBusiness { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetListUser(IDataTablesRequest dataTableParam, UserSearchViewModel searchViewModel)
        {
            return Json(UserBusiness.GetList(dataTableParam, searchViewModel));
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new UserAddViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(UserAddViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData.SetStatusMessage(GetModelErrorMessage(), UtilityEnum.StatusMessageType.Danger);
                return View(viewModel);
            }

            var result = UserBusiness.Add(viewModel);
            if (!result.IsSuccess)
            {
                TempData.SetStatusMessage(result.Message, UtilityEnum.StatusMessageType.Danger);
                return View(viewModel);
            }

            TempData.SetStatusMessage(result.Message);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var viewModel = new UserEditViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditViewModel viewModel, int id)
        {
            if (!ModelState.IsValid)
            {
                TempData.SetStatusMessage(GetModelErrorMessage(), UtilityEnum.StatusMessageType.Danger);
                return View(viewModel);
            }

            var result = UserBusiness.Edit(viewModel, id);
            if (!result.IsSuccess)
            {
                TempData.SetStatusMessage(result.Message, UtilityEnum.StatusMessageType.Danger);
                return View(viewModel);
            }

            TempData.SetStatusMessage(result.Message);
            return RedirectToAction("Index");
        }
    }
}