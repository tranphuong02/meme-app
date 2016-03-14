using System.Web.Mvc;
using Framework.Datatable.RequestBinder;
using Microsoft.Practices.Unity;
using Transverse;
using Transverse.Interfaces.Business;
using Transverse.Models.Business.User;
using Transverse.Security;

namespace Web.Areas.Administrator.Controllers.WEB
{
    [CustomAuthorize(Roles = Constants.RoleName.SuperAdmin + "," + Constants.RoleName.Admin)]
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
            InitAdd(viewModel);
            return View(viewModel);
        }

        // Helper Methods
        private void InitAdd(UserAddViewModel viewModel)
        {
            viewModel.Roles = RoleBusiness.GetAll();
        }
    }
}