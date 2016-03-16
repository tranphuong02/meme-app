using Microsoft.Practices.Unity;
using System.Web.Http;
using Transverse;
using Transverse.Interfaces.Business;
using Transverse.Models.Business.User;
using Transverse.Security;

namespace Web.Areas.Administrator.Controllers.API
{
    [RoutePrefix("api/UserApi")]
    [CustomAuthorize(Roles = Constants.RoleName.SuperAdmin)]
    public class UserApiController : BaseApiController
    {
        [Dependency]
        public IUserBusiness UserBusiness { get; set; }

        [HttpPost]
        [Route("Active")]
        public IHttpActionResult Active([FromBody] UserActiveViewModel viewModel)
        {
            var result = UserBusiness.Active(viewModel.Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("Deactive")]
        public IHttpActionResult Deactive([FromBody] UserDeactiveViewModel viewModel)
        {
            var result = UserBusiness.Deactive(viewModel.Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IHttpActionResult Delete([FromBody] UserDeleteViewModel viewModel)
        {
            var result = UserBusiness.Delete(viewModel.Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("ResetPassword")]
        public IHttpActionResult ResetPassword([FromBody] ResetPasswordViewModel viewModel)
        {
            var result = UserBusiness.ResetPassword(viewModel.Id);
            return Ok(result);
        }
    }
}