using Framework.Logger.Log4Net;
using Microsoft.Practices.Unity;
using System;
using System.Net;
using Transverse;
using Transverse.Interfaces.Business;
using Transverse.Interfaces.DAL;
using Transverse.Models.Business;
using Transverse.Models.Business.Account;
using Transverse.Utils;

namespace BusinessLogic
{
    public class UserBusiness : IUserBusiness
    {
        [Dependency]
        public IUserRepository UserRepository { get; set; }

        [Dependency]
        public IRoleRepository RoleRepository { get; set; }

        [Dependency]
        public IDbContext DbContext { get; set; }

        public BaseModel Authenticate(LoginViewModel viewModel)
        {
            try
            {
                var user = UserRepository.GetByEmail(viewModel.Email);
                if (user == null || string.Equals(BackendHelpers.CreatePasswordHash(viewModel.Password, user.PasswordSalt), user.PasswordHash))
                {
                    return new BaseModel(false, (int)HttpStatusCode.BadRequest, Constants.Message.InvalidLogin);
                }
                return new BaseModel(true, (int) HttpStatusCode.OK, user);
            }
            catch (Exception ex)
            {
                Provider.Instance.LogError(ex);
                return new BaseModel(false, (int)HttpStatusCode.InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}