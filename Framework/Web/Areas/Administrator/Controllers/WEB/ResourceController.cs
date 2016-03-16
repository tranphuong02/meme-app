using Framework.Datatable.RequestBinder;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Transverse;
using Transverse.Interfaces.Business;
using Transverse.Models.Business.Resource;
using Transverse.Security;

namespace Web.Areas.Administrator.Controllers.WEB
{
    [CustomAuthorize(Roles = Constants.RoleName.SuperAdmin + "," + Constants.RoleName.Admin)]
    public class ResourceController : BaseController
    {
        [Dependency]
        public IResourceBusiness ResourceBusiness { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetListGenre(IDataTablesRequest dataTableParam, ResourceGenreSearchViewModel searchViewModel)
        {
            return Json(ResourceBusiness.GetListGenre(dataTableParam, searchViewModel));
        }

        [HttpPost]
        public ActionResult GetListAuthor(IDataTablesRequest dataTableParam, ResourceAuthorSearchViewModel searchViewModel)
        {
            return Json(ResourceBusiness.GetListAuthor(dataTableParam, searchViewModel));
        }

        [HttpPost]
        public ActionResult GetListChapter(IDataTablesRequest dataTableParam, ResourceChapterSearchViewModel searchViewModel)
        {
            return Json(ResourceBusiness.GetListChapter(dataTableParam, searchViewModel));
        }
    }
}