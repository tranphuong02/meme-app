using Framework.Datatable.RequestBinder;
using Framework.Datatable.RequestParser;
using Framework.Logger.Log4Net;
using Framework.Utility;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Transverse.Enums;
using Transverse.Interfaces.Business;
using Transverse.Interfaces.DAL;
using Transverse.Models.Business.Account;
using Transverse.Models.Business.User;
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
                        FirstName = Constants.RoleName.SuperAdmin,
                        LastName = Constants.AppName,
                        Role = new Role
                        {
                            Name = Constants.RoleName.SuperAdmin
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

        public DataTablesResponse GetList(IDataTablesRequest dataTableParam, UserSearchViewModel searchViewModel)
        {
            try
            {
                var query = UserRepository.GetAll(x => x.IsDeleted == false, null, x => x.Role);

                if (searchViewModel.ActiveType != (int)ActiveType.All)
                {
                    switch (searchViewModel.ActiveType)
                    {
                        case (int)ActiveType.Active:
                            {
                                query = query.Where(x => x.IsActive);
                                break;
                            }

                        case (int)ActiveType.InActive:
                            {
                                query = query.Where(x => !x.IsActive);
                                break;
                            }
                    }
                }

                if (searchViewModel.RoleName != Constants.AllValue.ToString())
                {
                    query = query.Where(x => x.Role.Name == searchViewModel.RoleName);
                }

                var dataTableHelper = new DataTableHelper<UserViewModel, User>(query, x => new UserViewModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FirstName + " " + x.LastName,
                    Role = x.Role.Name,
                    IsActive = x.IsActive
                });

                var entities = dataTableHelper.GetDataVMForResponse(dataTableParam);
                var result = dataTableHelper.GetDataToList(dataTableParam, entities);

                return new DataTablesResponse(dataTableParam.Draw, result, entities.Count(), entities.Count());
            }
            catch (Exception ex)
            {
                Provider.Instance.LogError(ex);
                return new DataTablesResponse(dataTableParam.Draw, new List<UserViewModel>(), 0, 0);
            }
        }

        public BaseModel Add(UserAddViewModel viewModel)
        {
            try
            {
                var isExistsEmail = UserRepository.Any(x => x.IsDeleted == false && x.Email == viewModel.Email);
                if (isExistsEmail)
                {
                    return new BaseModel(false, (int)HttpStatusCode.BadRequest, string.Format(Constants.Message.IsExists, viewModel.Email));
                }

                var passwordSalt = BackendHelpers.CreateSaltKey();
                var passwordHash = BackendHelpers.CreatePasswordHash(Constants.DefaultPassword, passwordSalt);
                var user = new User
                {
                    Email = viewModel.Email,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    PasswordHash = passwordSalt,
                    PasswordSalt = passwordHash,
                    IsActive = viewModel.IsActive,
                    RoleId = viewModel.RoleId,
                    IsDeleted = false,
                    CreatedDate = DateTimeHelper.UTCNow()
                };

                UserRepository.Insert(user);
                DbContext.SaveChanges();

                return new BaseModel(true, (int) HttpStatusCode.OK, string.Format(Constants.Message.SuccessToAdd, "user"));
            }
            catch (Exception ex)
            {
               Provider.Instance.LogError(ex);
               return new BaseModel(false, (int)HttpStatusCode.InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }

        public BaseModel Edit(UserEditViewModel viewModel, int id)
        {
            try
            {
                var user = UserRepository.GetById(id);
                if (user == null || user.IsDeleted)
                {
                    return new BaseModel(false, (int) HttpStatusCode.BadRequest, string.Format(Constants.Message.IsNotExists, "user"));
                }

                var isExistsEmail = UserRepository.Any(x => x.IsDeleted == false && x.Email == viewModel.Email && x.Id != id);
                if (isExistsEmail)
                {
                    return new BaseModel(false, (int)HttpStatusCode.BadRequest, string.Format(Constants.Message.IsExists, viewModel.Email));
                }

                user.Email = viewModel.Email;
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                user.IsActive = viewModel.IsActive;
                user.RoleId = viewModel.RoleId;
                user.ModifiedDate = DateTimeHelper.UTCNow();

                UserRepository.Update(user);
                DbContext.SaveChanges();

                return new BaseModel(true, (int) HttpStatusCode.OK, string.Format(Constants.Message.SuccessToEdit, "user"));
            }
            catch (Exception ex)
            {
               Provider.Instance.LogError(ex);
               return new BaseModel(false, (int)HttpStatusCode.InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }

        public void InitAddViewModel(UserAddViewModel viewModel)
        {
            viewModel.Roles = RoleRepository.GetAll();
        }

        public void InitEditViewModel(UserEditViewModel viewModel, int id)
        {
            var user = UserRepository.GetById(id);
            if (user == null || user.IsDeleted)
            {
                return;
            }
            viewModel.Email = user.Email;
            viewModel.FirstName = user.FirstName;
            viewModel.LastName = user.LastName;
            viewModel.IsActive = user.IsActive;
            viewModel.RoleId = user.RoleId;
            viewModel.Roles = RoleRepository.GetAll();
        }

        private BaseModel UserNotFound()
        {
            return new BaseModel(false, (int)HttpStatusCode.BadRequest, @"Could not found user.");
        }
    }
}