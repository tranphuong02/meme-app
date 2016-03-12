using Framework.Logger.Log4Net;
using Microsoft.Practices.Unity;
using System;
using System.Linq;
using System.Net;
using Framework.Utility;
using Transverse.Interfaces.Business;
using Transverse.Interfaces.DAL;
using Transverse.Models.Business.Account;
using Transverse.Models.DAL;
using Transverse.Utils;
using BaseModel = Transverse.Models.Business.BaseModel;
using Constants = Transverse.Constants;

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
                // Supper admin
                if (viewModel.Email == BackendHelpers.SuperAdminEmail() && string.Equals(BackendHelpers.CreatePasswordHash(viewModel.Password, BackendHelpers.SuperAdminPasswordSalt()),BackendHelpers.SuperAdminPasswordHash()))
                {
                    var supperAdmin = new User
                    {
                        Email = BackendHelpers.SuperAdminEmail(),
                        FirstName = Constants.RoleName.SupperAdmin,
                        LastName = Constants.AppName,
                        Role = new Role
                        {
                            Name = Constants.RoleName.SupperAdmin
                        }
                    };

                    return new BaseModel(true, (int) HttpStatusCode.OK, supperAdmin);
                }

                // Normal user
                var user = UserRepository.GetByEmail(viewModel.Email);
                if (user == null || !string.Equals(BackendHelpers.CreatePasswordHash(viewModel.Password, user.PasswordSalt), user.PasswordHash))
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

        public BaseModel GetDataForgotPassword(string email)
        {
            try
            {
                var user = UserRepository.GetAll(x => x.IsActive && x.Email == email, null, x => x.Role).FirstOrDefault();

                if (user == null)
                {
                    return UserNotFound();
                }

                user.Token = Guid.NewGuid().ToString("N");
                user.ModifiedDate = DateTimeHelper.UTCNow();

                UserRepository.Update(user);
                DbContext.SaveChanges();

                return new BaseModel(true, (int) HttpStatusCode.OK, "Success.", new SendMailInfoVM
                {
                    Token = user.Token,
                    Email = user.Email
                });
            }
            catch (Exception ex)
            {
                Provider.Instance.LogError(ex);
    
                return new BaseModel(false, (int)HttpStatusCode.InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }

        public BaseModel ValidateToken(string email, string token)
        {
            try
            {
                var isExistsUser = UserRepository.Table.Any(x => x.Token == token && x.Email == email && x.IsDeleted == false && x.IsActive);

                if (isExistsUser == false)
                {
                    return new BaseModel(false, (int)HttpStatusCode.BadRequest, @"Token is invalid.");
                }

                return new BaseModel(true, (int)HttpStatusCode.OK, @"Success.");
            }
            catch (Exception ex)
            {
                Provider.Instance.LogError(ex);

                return new BaseModel(false, (int)HttpStatusCode.InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }

        public BaseModel ChangePassword(string email, string token, string newPassword)
        {
            try
            {
                var user = UserRepository.GetAll(x => x.Token == token && x.Email == email && x.IsDeleted == false && x.IsActive).FirstOrDefault();

                if (user == null)
                {
                    return UserNotFound();
                }

                user.PasswordSalt = BackendHelpers.CreateSaltKey();
                user.PasswordHash = BackendHelpers.CreatePasswordHash(newPassword, user.PasswordSalt);
                user.Token = string.Empty;
                user.ModifiedDate = DateTimeHelper.UTCNow();

                UserRepository.Update(user);
                DbContext.SaveChanges();

                return new BaseModel(true, (int) HttpStatusCode.OK, @"Your password has been updated successful.");
            }
            catch (Exception ex)
            {
                Provider.Instance.LogError(ex);

                return new BaseModel(false, (int)HttpStatusCode.InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }

        private BaseModel UserNotFound()
        {
            return new BaseModel(false, (int) HttpStatusCode.BadRequest, @"Could not found user.");
        }
    }
}